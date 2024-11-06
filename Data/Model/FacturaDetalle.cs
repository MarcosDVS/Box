using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Data.Request;
using Data.Response;

namespace Data.Model;

public class FacturaDetalle
{
    [Key]
    public int Id { get; set; }
    public int FacturaId { get; set; }
    public int ProductoId { get; set; }
    public int Cantidad { get; set; } = 1;
    [Column(TypeName = "decimal(18,2)")]
    public decimal Precio { get; set; }
    public decimal Descuento { get; set; }

    #region Relaciones
    [ForeignKey(nameof(FacturaId))]
    public virtual Factura Factura { get; set; }
    [ForeignKey(nameof(ProductoId))]
    public virtual Producto Producto { get; set; }
    #endregion

    [NotMapped]
    public decimal SubTotal => Cantidad * Precio;
    [NotMapped]
    public decimal TotalDesc => SubTotal * (Descuento / 100 );

    public static FacturaDetalle Crear(FacturaDetalleRequest request)
    => new()
    {
        ProductoId = request.ProductoId,
        Cantidad = request.Cantidad,
        Precio = request.Precio,
        Descuento = request.Descuento
    };
    
    public FacturaDetalleResponse ToResponse()
        => new()
        {
            Id = Id,
            Producto = Producto.ToResponse(),
            Cantidad = Cantidad,
            Precio = Precio,
            Descuento = Descuento,
            FacturaId = FacturaId
        };
}
