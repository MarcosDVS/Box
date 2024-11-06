using Data.Response;
using Data.Request;
using System.ComponentModel.DataAnnotations;

namespace Data.Model;

public class Proveedor
{
    [Key]
    public int Id { get; set; }
    public string NombreEmp { get; set; } = null!;
    public string? Email { get; set; }
    public string Telefono { get; set; } = null!;
    public string? Direccion { get; set; }

    public ProveedorResponse? ToResponse() => new()
    {
        Id = Id,
        NombreEmp = NombreEmp,
        Email = Email,
        Telefono = Telefono,
        Direccion = Direccion,
    };

    public static Proveedor Crear(ProveedorRequest item)
        => new()
        {
            NombreEmp = item.NombreEmp,
            Email = item.Email,
            Telefono = item.Telefono,
            Direccion = item.Direccion
        };
        public bool Mofidicar(ProveedorRequest item)
        {
            var cambio = false;
            if(NombreEmp != item.NombreEmp)
            {
                NombreEmp = item.NombreEmp;
                cambio = true;
            }
            if (Email != item.Email)
            {
                Email = item.Email;
                cambio = true;
            }
            if (Telefono != item.Telefono)
            {
                Telefono = item.Telefono;
                cambio = true;
            }
            if(Direccion != item.Direccion)
            {
                Direccion = item.Direccion;
                cambio = true;
            }
            return cambio;
        }
}