using Data.Model;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics.Contracts;
namespace Data.Context;

public class MyDbContext : DbContext, IMyDbContext
{
    public MyDbContext(DbContextOptions options) : base(options)
    {

    }

    public DbSet<Cliente> Clientes { get; set; }
    public DbSet<Proveedor> Proveedores { get; set; }
    public DbSet<Producto> Productos { get; set; }
    public DbSet<Categoria> Categorias { get; set; }
    public DbSet<Factura> Facturas { get; set; }
    public DbSet<FacturaDetalle> FacturaDetalles { get; set; }
    public DbSet<CuadrarCaja> CuadrarCajas { get; set; }
    public DbSet<Pago> Pagos { get; set; }
    public DbSet<Usuario> Usuarios { get; set; }

    public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        return base.SaveChangesAsync(cancellationToken);
    }

}

public interface IMyDbContext
    {
        DbSet<Categoria> Categorias { get; set; }
        DbSet<Cliente> Clientes { get; set; }
        DbSet<FacturaDetalle> FacturaDetalles { get; set; }
        DbSet<Factura> Facturas { get; set; }
        DbSet<Pago> Pagos { get; set; }
        DbSet<CuadrarCaja> CuadrarCajas { get; set; }
        DbSet<Producto> Productos { get; set; }
        DbSet<Proveedor> Proveedores { get; set; }
        DbSet<Usuario> Usuarios { get; set; }

        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    }