using Data.Context;
using Data.Model;
using Data.Response;
using Data.Request;
using Microsoft.EntityFrameworkCore;

namespace Data.Services;

public class CuadrarCajaServices : ICuadrarCajaServices
{
    private readonly IMyDbContext dbContext;

    public CuadrarCajaServices(IMyDbContext dbContext)
    {
        this.dbContext = dbContext;
    }

    public async Task<Result<List<CuadrarCajaResponse>>> Consultar(string filtro)
    {
        try
        {
            var item = await dbContext.CuadrarCajas
                .Include(c => c.Cashier)
                .Where(c =>
                    (c.Cashier.Nombre + "" + c.Cashier.Apellidos)
                    .ToLower()
                    .Contains(filtro.ToLower()
                    )
                )
                .Select(c => c.ToResponse())
                .ToListAsync();
            return new Result<List<CuadrarCajaResponse>>()
            {
                Message = "Ok",
                Success = true,
                Data = item!
            };
        }
        catch (Exception E)
        {
            return new Result<List<CuadrarCajaResponse>>
            {
                Message = E.Message,
                Success = false
            };
        }
    }

    public async Task<Result> Crear(CuadrarCajaRequest request)
    {
        try
        {
            var item = CuadrarCaja.Crear(request);
            dbContext.CuadrarCajas.Add(item);
            await dbContext.SaveChangesAsync();
            return new Result() { Message = "Ok", Success = true };
        }
        catch (Exception E)
        {

            return new Result() { Message = E.Message, Success = false };
        }
    }
    
    public async Task<Result<CuadrarCajaResponse>> Modificar(CuadrarCajaRequest request)
    {
        try
        {
            var caja = await dbContext.CuadrarCajas
                .FirstOrDefaultAsync(c => c.Id == request.Id);

            if (caja == null)
                return new Result<CuadrarCajaResponse>() { Message = "No se encontró la caja", Success = false };

            // Actualizar las propiedades de la caja según el request
            caja.CajeroId = request.CajeroId;
            caja.Monto = request.Monto;
            caja.MontoCuadrado = request.MontoCuadrado;

            caja.One = request.One;
            caja.Five = request.Five;
            caja.Ten = request.Ten;
            caja.TwentyFive = request.TwentyFive;
            caja.Fifty = request.Fifty;
            caja.OneHundred = request.OneHundred;
            caja.TwoHundred = request.TwoHundred;
            caja.FiveHundred = request.FiveHundred;
            caja.OneThousand = request.OneThousand;
            caja.TwoThousand = request.TwoThousand;
            
            await dbContext.SaveChangesAsync();

            return new Result<CuadrarCajaResponse>()
            {
                Data = caja.ToResponse(),
                Success = true,
                Message = "Factura modificada correctamente"
            };
        }
        catch (Exception ex)
        {
            return new Result<CuadrarCajaResponse>()
            {
                Data = null,
                Success = false,
                Message = ex.Message
            };
        }
    }

    public async Task<Result> Eliminar(CuadrarCajaRequest request)
    {
        try
        {
            var item = await dbContext.CuadrarCajas
                .FirstOrDefaultAsync(c => c.Id == request.Id);
            if (item == null)
                return new Result() { Message = "No se encontro el CuadrarCaja", Success = false };

            dbContext.CuadrarCajas.Remove(item);
            await dbContext.SaveChangesAsync();
            return new Result() { Message = "Ok", Success = true };
        }
        catch (Exception E)
        {

            return new Result() { Message = E.Message, Success = false };
        }
    }
}

public interface ICuadrarCajaServices
{
    Task<Result<List<CuadrarCajaResponse>>> Consultar(string filtro);
    Task<Result> Crear(CuadrarCajaRequest request);
    Task<Result<CuadrarCajaResponse>> Modificar(CuadrarCajaRequest request);
    Task<Result> Eliminar(CuadrarCajaRequest request);
}