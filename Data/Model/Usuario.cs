using System.ComponentModel.DataAnnotations;
using Data.Request;
using Data.Response;

namespace Data.Model;

public class Usuario
{
    [Key]
    public int Id { get; set; }
    public string Nombre { get; set; } = null!;
    public string Apellidos { get; set; } = null!;
    public string Email { get; set; } = null!;
    public string Password { get; set; } = null!;
    public string? Role { get; set; }
    public virtual ICollection<CuadrarCaja> Detalles { get; set; }

    public static Usuario Crear(UsuarioRequest user) => new()
    {
        Nombre = user.Nombre,
        Apellidos = user.Apellidos,
        Email = user.Email,
        Password = user.Password,
        Role = user.Role
    };
    public bool Modificar(UsuarioRequest user)
    {
        var cambio = false;
        if (Nombre != user.Nombre) Nombre = user.Nombre; cambio = true;
        if (Apellidos != user.Apellidos) Apellidos = user.Apellidos; cambio = true;
        if (Email != user.Email) Email = user.Email; cambio = true;
        if (Password != user.Password) Password = user.Password; cambio = true;
        if (Role != user.Role) Role = user.Role; cambio = true;

        return cambio;
    }
    public UsuarioResponse ToResponse() => new()
    {
        Id = Id,
        Nombre = Nombre,
        Apellidos = Apellidos,
        Email = Email,
        Password = Password,
        Role = Role
    };
}