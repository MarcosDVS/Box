using System.ComponentModel.DataAnnotations.Schema;

namespace Data.Response;

public class FacturaDetalleResponse
{
    public int Id { get; set; }
    public int FacturaId { get; set; }
    public ProductoResponse Producto { get; set; } = null!;
    public int Cantidad { get; set; }
    public decimal Precio { get; set; }
    public decimal Descuento { get; set; }

    [NotMapped]
    public decimal SubTotal => Cantidad * Precio;
    [NotMapped]
    public decimal TotalDesc => SubTotal * (Descuento / 100 );
    public decimal ITBIS => SubTotal * 0.18m;
    public decimal PrecioITBIS => Precio * 0.18m;
}
