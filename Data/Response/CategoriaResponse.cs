using Data.Request;

namespace Data.Response;

public class CategoriaResponse
{
    public int Id { get; set; }
    public string Nombre { get; set; } = null!;

    public string CodigoDescripcion => $"({Id}) {Nombre}";
    public CategoriaRequest ToRequest()
    {
        return new CategoriaRequest
        {
            Id = Id,
            Nombre = Nombre

        };
    }
}