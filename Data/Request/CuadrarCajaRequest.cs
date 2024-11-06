namespace Data.Request; // Sin el punto al final
public class CuadrarCajaRequest
{
    public int Id { get; set; }
    public DateTime Fecha { get; set; }
    // public string Cajero { get; set; } = null!;
    public int CajeroId { get; set; }
    public decimal VentaCredito { get; set; }
    public decimal VentaContado { get; set; }
    public decimal Abonado { get; set; }
    public decimal Monto { get; set; }
    public decimal MontoCuadrado { get; set; }

    public int One { get; set; }
    public int Five { get; set; }
    public int Ten { get; set; }
    public int TwentyFive { get; set; }
    public int Fifty { get; set; }
    public int OneHundred { get; set; }
    public int TwoHundred { get; set; }
    public int FiveHundred { get; set; }
    public int OneThousand { get; set; }
    public int TwoThousand { get; set; }
    
    public decimal Total => (One * 1) + (Five * 5) + (Ten * 10) + (TwentyFive * 25) + (Fifty * 50)
    + (OneHundred * 100) + (TwoHundred * 200) + (FiveHundred * 500) 
    + (OneThousand * 1000) + (TwoThousand * 2000);
}
