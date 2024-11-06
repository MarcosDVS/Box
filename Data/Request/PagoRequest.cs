using Data.Response;

namespace Data.Request;

public class PagoRequest
{
    public int Id { get; set; }
    public int FacturaID { get; set; }
    public DateTime Fecha { get; set; } = DateTime.Now;
    public double MontoPagado { get; set; }
    public string? Observacion { get; set; }
    public decimal Pendiente { get; set; }
    public virtual ICollection<FacturaRequest> Facturas { get; set; } 
        = new List<FacturaRequest>();
}