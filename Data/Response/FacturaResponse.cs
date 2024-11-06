using System.ComponentModel.DataAnnotations.Schema;
using Data.Request;

namespace Data.Response;

public class FacturaResponse
{
    public int Id { get; set; }
    public int ClienteId { get; set; }
    public DateTime Fecha { get; set; }
    public ClienteResponse Cliente { get; set; }
    public virtual ICollection<FacturaDetalleResponse> Detalles { get; set; }
    public virtual ICollection<PagoResponse> Pagos { get; set; }

    [NotMapped]
    public decimal SubTotal =>
        Detalles != null ? //IF
        Detalles.Sum(d => d.SubTotal) //Verdadero
        :
        0;//Falso

    [NotMapped]
    public decimal TotalDesc =>
        Detalles != null ? //IF
        Detalles.Sum(d => d.TotalDesc) //Verdadero
        :
        0;//Falso
    public string TypePayment  { get; set; } = null!;
    public decimal SaldoPagado { get; set; }
    public decimal Cambio => SaldoPagado - SubTotal - TotalDesc;
    public decimal SaldoPendiente => Pagos != null && Pagos.Any()
    ? SubTotal - (decimal)Pagos.Sum(p => p.MontoPagado) - SaldoPagado - TotalDesc
    : SubTotal - TotalDesc - SaldoPagado;

    public decimal DineroPagado => Pagos != null && Pagos.Any()
    ? SaldoPagado + (decimal)Pagos.Sum(p => p.MontoPagado)
    : SaldoPagado;

    public decimal ITBIS => SubTotal * 0.18m;
    
    public FacturaRequest ToRequest()
    {   
        return new FacturaRequest
        {
            Id = Id,
            ClienteId = ClienteId,
            TypePayment = TypePayment,
            SaldoPagado = SaldoPagado,
            Fecha = Fecha,
            Detalles = Detalles.Select(d => new FacturaDetalleRequest
            {
                Id = d.Id,
                ProductoId = d.Producto.Id, // Asegúrate de que esto sea correcto
                Descripcion = d.Producto.Nombre, // Asegúrate de que esto sea correcto
                Cantidad = d.Cantidad,
                Precio = d.Precio
            }).ToList()
        };
    }
}
