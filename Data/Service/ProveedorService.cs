using Data.Context;
using Data.Model;
using Data.Response;
using Data.Request;
using Microsoft.EntityFrameworkCore;

namespace Data.Services;

public class ProveedorServices : IProveedorServices
{
    private readonly IMyDbContext dbContext;

    public ProveedorServices(IMyDbContext dbContext)
    {
        this.dbContext = dbContext;
    }

    public async Task<Result<List<ProveedorResponse>>> Consultar(string filtro)
    {
        try
        {
            var contactos = await dbContext.Proveedores
                .Where(c =>
                    (c.NombreEmp)
                    .ToLower()
                    .Contains(filtro.ToLower()
                    )
                )
                .Select(c => c.ToResponse())
                .ToListAsync();
            return new Result<List<ProveedorResponse>>()
            {
                Message = "Ok",
                Success = true,
                Data = contactos
            };
        }
        catch (Exception E)
        {
            return new Result<List<ProveedorResponse>>
            {
                Message = E.Message,
                Success = false
            };
        }
    }

    public async Task<Result> Crear(ProveedorRequest request)
    {
        try
        {
            var contacto = Proveedor.Crear(request);
            dbContext.Proveedores.Add(contacto);
            await dbContext.SaveChangesAsync();
            return new Result() { Message = "Ok", Success = true };
        }
        catch (Exception E)
        {

            return new Result() { Message = E.Message, Success = false };
        }
    }
    public async Task<Result> Modificar(ProveedorRequest request)
    {
        try
        {
            var contacto = await dbContext.Proveedores
                .FirstOrDefaultAsync(c => c.Id == request.Id);
            if (contacto == null)
                return new Result() { Message= "No se encontro el proveedor", Success = false };

            if (contacto.Mofidicar(request))
                await dbContext.SaveChangesAsync();

            return new Result() { Message = "Ok", Success = true };
        }
        catch (Exception E)
        {

            return new Result() { Message = E.Message, Success = false };
        }
    }

    public async Task<Result> Eliminar(ProveedorRequest request)
    {
        try
        {
            var contacto = await dbContext.Proveedores
                .FirstOrDefaultAsync(c => c.Id == request.Id);
            if (contacto == null)
                return new Result() { Message = "No se encontro el proveedor", Success = false };

            dbContext.Proveedores.Remove(contacto);
            await dbContext.SaveChangesAsync();
            return new Result() { Message = "Ok", Success = true };
        }
        catch (Exception E)
        {

            return new Result() { Message = E.Message, Success = false };
        }
    }
    public async Task CrearSuplidor()
    {
        var item = await dbContext.Proveedores.FirstOrDefaultAsync(u => u.NombreEmp == "Desconocido");

        if (item == null)
        {
            item = new Proveedor
            {
                NombreEmp = "Desconocido",
                Email = "Desconocido",
                Direccion = "Desconocida",
                Telefono = "Desconocido"
            };

            dbContext.Proveedores.Add(item);
            await dbContext.SaveChangesAsync();
        }
    }
}

public interface IProveedorServices
{
    Task<Result<List<ProveedorResponse>>> Consultar(string filtro);
    Task<Result> Crear(ProveedorRequest request);
    Task<Result> Modificar(ProveedorRequest request);
    Task<Result> Eliminar(ProveedorRequest request);
    Task CrearSuplidor();
}