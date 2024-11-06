using Data.Response;
using Data.Request;
using System.ComponentModel.DataAnnotations;

namespace Data.Model;

public class Categoria
{
    [Key]
    public int Id { get; set; }
    public string Nombre { get; set; } = null!;

    public CategoriaResponse? ToResponse() => new()
    {
        Id = Id,
        Nombre = Nombre
    };
    public static Categoria Crear(CategoriaRequest item)
    => new()
    {
        Nombre = item.Nombre
    };
    public bool Mofidicar(CategoriaRequest item)
    {
        var cambio = false;
        if(Nombre != item.Nombre)
        {
            Nombre = item.Nombre;
            cambio = true;
        }
        return cambio;
    }
}
