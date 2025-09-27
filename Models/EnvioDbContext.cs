using Microsoft.EntityFrameworkCore;

namespace ModeloParcialApi.Models
{
    public class EnvioDbContext : DbContext
    {
        public EnvioDbContext(DbContextOptions<EnvioDbContext> options) : base(options)
        {
            
        }

        public DbSet<Envio> TEnvios {get; set;}
        public DbSet<Envio> TDetalles {get; set;}
        public DbSet<Envio> TProducto {get; set;}


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<DetalleEnvio>()
                .HasKey(d => d.IdDetalle);
            modelBuilder.Entity<DetalleEnvio>()
                .HasOne(d => d.Producto)
                .WithMany(p => p.Detalles)
                .HasForeignKey(d => d.IdProducto);

            modelBuilder.Entity<Producto>()
                .HasKey(d => d.IdProducto);
        }
    }
}