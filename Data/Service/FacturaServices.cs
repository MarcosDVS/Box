using Data.Context;
using Data.Model;
using Data.Request;
using Data.Response;
using Microsoft.EntityFrameworkCore;

namespace Data.Services;

public class FacturaServices : IFacturaServices
{
    private readonly IMyDbContext dbContext;

    public FacturaServices(IMyDbContext dbContext)
    {
        this.dbContext = dbContext;
    }
    public async Task<Result<List<FacturaResponse>>> Consultar(string filtro)
    {
        try
        {
            var facturas = await dbContext.Facturas
                .Where(c =>
                        (c.Id + " " + c.Cliente.Nombre + " " + c.Cliente.Apellidos)
                        .ToLower()
                        .Contains(filtro.ToLower())
                )
                .AsNoTracking()
                .IgnoreAutoIncludes()
                .Include(f => f.Cliente)
                .Include(f => f.Pagos)
                .Include(f => f.Detalles)
                .ThenInclude(d => d.Producto)
                .Select(f => f.ToResponse())
                .ToListAsync();
            return new Result<List<FacturaResponse>>()
            {
                Data = facturas,
                Success = true,
                Message = "Ok"
            };
        }
        catch (Exception E)
        {
            return new Result<List<FacturaResponse>>()
            {
                Data = null,
                Success = false,
                Message = E.Message
            };
        }
    }

    public async Task<Result<FacturaResponse>> Crear(FacturaRequest request)
    {
        try
        {
            var factura = Factura.Crear(request);
            dbContext.Facturas.Add(factura);
            await dbContext.SaveChangesAsync();
            return new Result<FacturaResponse>()
            {
                Data = factura.ToResponse(),
                Success = true,
                Message = "Ok ✔"
            };
        }
        catch (Exception E)
        {
            return new Result<FacturaResponse>()
            {
                Data = null,
                Success = false,
                Message = E.Message
            };
        }
    }
    public async Task<Result> Eliminar(FacturaRequest request)
    {
        try
        {
            var contacto = await dbContext.Facturas
                .FirstOrDefaultAsync(c => c.Id == request.Id);
            if (contacto == null)
                return new Result() { Message = "No se encontro el usuario", Success = false };

            dbContext.Facturas.Remove(contacto);
            await dbContext.SaveChangesAsync();
            return new Result() { Message = "Ok", Success = true };
        }
        catch (Exception E)
        {

            return new Result() { Message = E.Message, Success = false };
        }
    }
   
    public async Task<Result<FacturaResponse>> Modificar(FacturaRequest request)
    {
        try
        {
            var factura = await dbContext.Facturas
                .Include(f => f.Detalles)
                .ThenInclude(d => d.Producto)
                .Include(f => f.Pagos)
                .FirstOrDefaultAsync(c => c.Id == request.Id);

            if (factura == null)
                return new Result<FacturaResponse>() { Message = "No se encontró la factura", Success = false };

            // Actualizar las propiedades de la factura según el request
            factura.ClienteId = request.ClienteId;
            factura.TypePayment = request.TypePayment;
            factura.SaldoPagado = request.SaldoPagado;
            factura.SaldoPendiente = request.SaldoPendiente;
            factura.Detalles = request.Detalles
                .Select(detalle => FacturaDetalle.Crear(detalle))
                .ToList();

            // Guardar los cambios en la base de datos
            await dbContext.SaveChangesAsync();

            return new Result<FacturaResponse>()
            {
                Data = factura.ToResponse(),
                Success = true,
                Message = "Factura modificada correctamente"
            };
        }
        catch (Exception ex)
        {
            return new Result<FacturaResponse>()
            {
                Data = null,
                Success = false,
                Message = ex.Message
            };
        }
    }

    public async Task<bool> StokDevuelto(int itemId, ProductoRequest detalle)
    {
        try
        {
            var factura = await dbContext.FacturaDetalles
                .FirstOrDefaultAsync(p => p.ProductoId == itemId);

            if (factura != null)
            {
                // Resta la cantidad del detalle al stock del producto
                factura.Cantidad += detalle.Stock;
                await dbContext.SaveChangesAsync();
                return true;
            }

            return false; // No se encontró el producto con el ID especificado
        }
        catch (Exception)
        {
            return false;
        }
    }

}

public interface IFacturaServices
{
    Task<Result<List<FacturaResponse>>> Consultar(string filtro);
    Task<Result<FacturaResponse>> Crear(FacturaRequest request);
    Task<Result> Eliminar(FacturaRequest request);
    Task<Result<FacturaResponse>> Modificar(FacturaRequest request);
    Task<bool> StokDevuelto(int itemId, ProductoRequest detalle);
}