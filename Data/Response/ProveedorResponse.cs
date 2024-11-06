using Data.Request;

namespace Data.Response;

public class ProveedorResponse
{
    public int Id { get; set; }
    public string NombreEmp { get; set; } = null!;
    public string? Email { get; set; }
    public string Telefono { get; set; } = null!;
    public string? Direccion { get; set; }

    public ProveedorRequest ToRequest()
    {
        return new ProveedorRequest
        {
            Id = Id,
            NombreEmp = NombreEmp,
            Email = Email,
            Telefono = Telefono,
            Direccion = Direccion
        };
    }
}
