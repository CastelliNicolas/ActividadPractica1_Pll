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
        public void Delete(int id)
        {
            var factura = _dbContext.Facturas.Find(id);
            if (factura != null)
            {
                var detalles = _dbContext.DetalleFacturas.Where(d => d.NroFactura == id);
                _dbContext.DetalleFacturas.RemoveRange(detalles);
                _dbContext.Facturas.Remove(factura);
                _dbContext.SaveChanges();
            }
        }

        public List<Factura> GetAll()
        {
            return _dbContext.Facturas
                .Include(f => f.DetalleFacturas)
                    .ThenInclude(d => d.IdArticuloNavigation)
                .Include(f => f.IdFormaPagoNavigation)
                .ToList();
        }

        public Factura? GetById(int id)
        {
            return _dbContext.Facturas
                .Include(f => f.DetalleFacturas)
                    .ThenInclude(d => d.IdArticuloNavigation)
                .Include(f => f.IdFormaPagoNavigation)
                .FirstOrDefault(f => f.NroFactura == id);
        }

        public void Insert(Factura factura)
        {
            if (factura.DetalleFacturas != null && factura.DetalleFacturas.Count > 0)
            {
                _dbContext.Facturas.Add(factura);
                _dbContext.SaveChanges();
            }
        }

        public void Update(Factura factura, int nroFactura)
        {
            var facturaExistente = _dbContext.Facturas.Find(nroFactura);
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
            _dbContext.SaveChanges();
        }
    }
}