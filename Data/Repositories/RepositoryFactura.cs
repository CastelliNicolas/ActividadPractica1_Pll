using ActividadPractica3.Models;
using Microsoft.EntityFrameworkCore;

namespace ActividadPractica3.Data.Repositories
{
    public class RepositoryFactura : IRepositoryFactura
    {

        private readonly DBContext _dbContext;
        public RepositoryFactura(DBContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task Delete(int id)
        {
            var factura = await _dbContext.Facturas.FindAsync(id);
            if (factura != null)
            {
                var detalles = await _dbContext.DetalleFacturas
                                               .Where(d => d.NroFactura == id)
                                               .ToListAsync();
                _dbContext.DetalleFacturas.RemoveRange(detalles);
                _dbContext.Facturas.Remove(factura);
                await _dbContext.SaveChangesAsync();
            }
        }

        public async Task<List<Factura>> GetAll()
        {
            return await _dbContext.Facturas
                .Include(f => f.DetalleFacturas)
                    .ThenInclude(d => d.IdArticuloNavigation)
                .Include(f => f.IdFormaPagoNavigation)
                .ToListAsync();
        }

        public async Task<Factura?> GetById(int id)
        {
            return await _dbContext.Facturas
                .Include(f => f.DetalleFacturas)
                    .ThenInclude(d => d.IdArticuloNavigation)
                .Include(f => f.IdFormaPagoNavigation)
                .FirstOrDefaultAsync(f => f.NroFactura == id);
        }

        public async Task Insert(Factura factura)
        {
            if (factura.DetalleFacturas != null && factura.DetalleFacturas.Count > 0)
            {
                await _dbContext.Facturas.AddAsync(factura);
                await _dbContext.SaveChangesAsync();
            }
        }

        public async Task Update(Factura factura, int nroFactura)
        {
            var facturaExistente = await _dbContext.Facturas
                                                   .Include(d => d.DetalleFacturas)
                                                   .FirstOrDefaultAsync(f => f.NroFactura == nroFactura);
            if (facturaExistente != null) 
            {
                facturaExistente.Fecha = factura.Fecha;
                facturaExistente.IdFormaPago = factura.IdFormaPago;
                facturaExistente.Cliente = factura.Cliente;
                foreach (var detalle in factura.DetalleFacturas)
                {
                    var detalleExistente = facturaExistente.DetalleFacturas
                        .FirstOrDefault(d => d.IdDetalle == detalle.IdDetalle);

                    if (detalleExistente != null)
                    {
                        detalleExistente.IdArticulo = detalle.IdArticulo;
                        detalleExistente.Cantidad = detalle.Cantidad;
                    }
                    else
                    {
                        var nuevoDetalle = new DetalleFactura
                        {
                            NroFactura = facturaExistente.NroFactura,
                            IdArticulo = detalle.IdArticulo,
                            Cantidad = detalle.Cantidad
                        };
                        facturaExistente.DetalleFacturas.Add(nuevoDetalle);
                    }
                }
            }
            await _dbContext.SaveChangesAsync();
        }
    }
}