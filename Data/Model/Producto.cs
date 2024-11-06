using Data.Request;
using Data.Response;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.Contracts;

namespace Data.Model;

public class Producto
{
    [Key]
    public int Id { get; set; }
    public string? Codigo { get; set; }
    // public int ProveedorID { get; set; }
    public string Nombre { get; set; } = null!;
    public int Stock { get; set; } 
    public int CategoriaID { get; set; }
    [Column(TypeName = "decimal(18,2)")]
    public decimal Precio { get; set; }

    // [ForeignKey("ProveedorID")]
    // public virtual Proveedor? Proveedor { get; set; }
    [ForeignKey("CategoriaID")]
    public virtual Categoria? Categoria { get; set; }

    public static Producto Crear(ProductoRequest item)
        => new()
        {
            Codigo = item.Codigo,
            // ProveedorID = item.ProveedorID,
            Nombre = item.Nombre,
            Stock = item.Stock,
            CategoriaID = item.CategoriaID,
            Precio = item.Precio
        };

    public bool Mofidicar(ProductoRequest item)
    {
        var cambio = false;
        if (Codigo != item.Codigo)
        {
            Codigo = item.Codigo;
            cambio = true;
        }
        // if (ProveedorID != item.ProveedorID)
        // {
        //     ProveedorID = item.ProveedorID;
        //     cambio = true;
        // }
        if (Stock != item.Stock)
        {
            Stock = item.Stock;
            cambio = true;
        }
        if (Nombre != item.Nombre)
        {
            Nombre = item.Nombre;
            cambio = true;
        }
        if (CategoriaID != item.CategoriaID)
        {
            CategoriaID = item.CategoriaID;
            cambio = true;
        }
        if (Precio != item.Precio)
        {
            Precio = item.Precio;
            cambio = true;
        }
        return cambio;
    }

    public ProductoResponse ToResponse()
        => new()
        {
            Id = Id,
            Codigo = Codigo,
            Nombre = Nombre,
            Stock = Stock,
            Precio = Precio,
            CategoriaID = CategoriaID,
            Categoria = Categoria != null? Categoria!.ToResponse():null,
            // ProveedorID = ProveedorID,
            // Proveedor = Proveedor != null? Proveedor!.ToResponse():null
        };
}