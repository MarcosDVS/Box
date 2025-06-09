using Microsoft.EntityFrameworkCore;
//using FactuSystem.Authentication;
using Data.Context;
using Data.Model;
using Data.Request;
using Data.Response;

namespace Data.Services;
public class UsuarioServices : IUsuarioServices
{
    #region Constructor y mienbro privado
    private MyDbContext _database;

    public UsuarioServices(MyDbContext database)
    {
        _database = database;
    }
    #endregion
    //Tarea para registrar un Vehiculo nuevo en la base de Data...
    #region Funciones
    public async Task<Result> Crear(UsuarioRequest request)
    {
        try
        {
            var contacto = Usuario.Crear(request);
            _database.Usuarios.Add(contacto);
            await _database.SaveChangesAsync();
            return new Result() { Message = "Ok", Success = true };
        }
        catch (Exception E)
        {

            return new Result() { Message = E.Message, Success = false };
        }
    }
    public async Task<Result> Modificar(UsuarioRequest request)
    {
        try
        {
            var usuario = await _database.Usuarios
                .FirstOrDefaultAsync(c => c.Id == request.Id);
            if (usuario == null)
                return new Result() { Message = "No se encontro el gasto", Success = false };

            if (usuario.Modificar(request))
                await _database.SaveChangesAsync();

            return new Result() { Message = "Ok", Success = true };
        }
        catch (Exception E)
        {

            return new Result() { Message = E.Message, Success = false };
        }
    }
    public async Task<Result> Eliminar(UsuarioRequest request)
    {
        try
        {
            var contacto = await _database.Usuarios
                .FirstOrDefaultAsync(c => c.Id == request.Id);
            if (contacto == null)
                return new Result() { Message = "No se encontro el usuario", Success = false };

            _database.Usuarios.Remove(contacto);
            await _database.SaveChangesAsync();
            return new Result() { Message = "Ok", Success = true };
        }
        catch (Exception E)
        {

            return new Result() { Message = E.Message, Success = false };
        }
    }
    public async Task<Result<List<UsuarioResponse>>> Consultar(string filtro)
    {
        try
        {
            var usuarios = await _database.Usuarios
                .Where(u =>
                    (u.Nombre + " "+ u.Apellidos+" "+ u.Email + " "+ u.Password+" "+u.Role)
                    .ToLower()
                    .Contains(filtro.ToLower()
                    )
                )
                .Select(u => u.ToResponse())
                .ToListAsync();
            return new Result<List<UsuarioResponse>>()
            {
                Message = "Ok",
                Success = true,
                Data = usuarios
            };
        }
        catch (Exception E)
        {
            return new Result<List<UsuarioResponse>>
            {
                Message = E.Message,
                Success = false
            };
        }
    }
    #endregion

    public async Task<Result<Usuario>> Login(string username, string password)
    {
        try
        {
            var user = await _database.Usuarios
                .FirstOrDefaultAsync(u => u.Email == username && u.Password == password);

            if (user != null)
            {
                return new Result<Usuario>
                {
                    Success = true,
                    Data = user,
                    Message = "Inicio de sesión Success"
                };
            }
            else
            {
                return new Result<Usuario>
                {
                    Success = false,
                    Message = "Credenciales de inicio de sesión incorrectas"
                };
            }
        }
        catch (Exception ex)
        {
            return new Result<Usuario>
            {
                Success = false,
                Message = $"Error en el inicio de sesión: {ex.Message}"
            };
        }
    }
    public async Task CrearUsuarioAdmin()
    {
        var adminUser = await _database.Usuarios.FirstOrDefaultAsync(u => u.Email == "admin");

        if (adminUser == null)
        {
            adminUser = new Usuario
            {
                Nombre = "System",
                Apellidos = "Administrator",
                Email = "admin",
                Password = "1234", // Recuerda realizar un hash de la contraseña en un entorno de producción
                Role = "Admin"
            };

            _database.Usuarios.Add(adminUser);
            await _database.SaveChangesAsync();
        }
    }
    
}


public interface IUsuarioServices
{
    Task<Result<List<UsuarioResponse>>> Consultar(string filtro);
    Task<Result> Crear(UsuarioRequest request);
    Task<Result> Eliminar(UsuarioRequest request);
    Task<Result> Modificar(UsuarioRequest request);
    Task<Result<Usuario>> Login(string username, string password);
    Task CrearUsuarioAdmin();
}