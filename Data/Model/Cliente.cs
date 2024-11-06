using System.ComponentModel.DataAnnotations;
using Data.Request;
using Data.Response;
using Microsoft.Win32;

namespace Data.Model;

public class Cliente
{
    [Key]
    public int Id { get; set; }

    public string? Cedula { get; set; }

    public string Nombre { get; set; } = null!;

    public string Apellidos { get; set; } = null!;
    
    public string? Direccion { get; set; }

    public string? Telefono { get; set; }

    public string? Correo { get; set; }
    public static Cliente Crear(ClienteRequest cliente)
    {
        return new Cliente()
        {
            Nombre = cliente.Nombre,
            Apellidos = cliente.Apellidos,
            Direccion = cliente.Direccion,
            Telefono = cliente.Telefono,
            Cedula = cliente.Cedula,
            Correo = cliente.Correo,
        };
    }

    public bool Modificar(ClienteRequest cliente)
    {
        var cambio = false;

        if (Nombre != cliente.Nombre)
        {
            Nombre = cliente.Nombre;
            cambio = true;
        }
        if (Apellidos != cliente.Apellidos)
        {
            Apellidos = cliente.Apellidos;
            cambio = true;
        }
        if (Direccion != cliente.Direccion)
        {
            Direccion = cliente.Direccion;
            cambio = true;
        }
        if (Telefono != cliente.Telefono)
        {
            Telefono = cliente.Telefono;
            cambio = true;
        }
        if (Cedula != cliente.Cedula)
        {
            Cedula = cliente.Cedula;
            cambio = true;
        }
        if (Correo != cliente.Correo)
        {
            Correo = cliente.Correo;
            cambio = true;
        }

        return cambio;
    }



    public ClienteResponse? ToResponse()
    {
        return new ClienteResponse()
        {
            Id = Id,
            Cedula = Cedula,
            Nombre = Nombre,
            Apellidos = Apellidos,
            Telefono = Telefono,
            Direccion = Direccion,
            Correo = Correo,
        };
    }
}
