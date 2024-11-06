namespace Data.Request;

public class ProveedorRequest
{
    public int Id { get; set; }
    public string NombreEmp { get; set; } = null!;
    public string? Email { get; set; }
    public string Telefono { get; set; } = null!;
    public string? Direccion { get; set; }
}
