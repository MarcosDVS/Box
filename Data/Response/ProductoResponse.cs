using Data.Model;
using Data.Request;

namespace Data.Response;

public class ProductoResponse
{
    public int Id { get; set; }
    public string? Codigo { get; set; }
    // public int ProveedorID { get; set; }
    public string Nombre { get; set; } = null!;
    public int Stock { get; set; }
    public int CategoriaID { get; set; }
    public decimal Precio { get; set; }
    public ProveedorResponse? Proveedor { get; set; }
    public CategoriaResponse? Categoria { get; set; }
    
    public string? ImagenRuta { get; set; }

    public string NombreProveedortexto => Proveedor != null ? Proveedor.NombreEmp : "N/A";
    public string NombreCategoriatexto => Categoria != null ? Categoria.Nombre : "N/A";

    public string CodigoDescripcion => $"({Codigo}) {Nombre}";

    public ProductoRequest ToRequest() 
    {
        return new ProductoRequest
        {
            Id = Id,
            Codigo = Codigo,
            Nombre = Nombre,
            Stock = Stock,
            CategoriaID = CategoriaID,
            // ProveedorID = ProveedorID,
            Precio = Precio,
            ImagenRuta = ImagenRuta
        };
    }
}
