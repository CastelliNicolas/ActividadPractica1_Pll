using Microsoft.EntityFrameworkCore;
using ModeloParcialApi.Models;

namespace ModeloParcialApi.Repositories
{
    public class EnvioRepository : IEnvioRepository
    {
        private readonly EnvioDbContext _context;
        public EnvioRepository(EnvioDbContext context)
        {
            _context = context;
        }

        public List<Envio> GetAll(string direccion, string? estado)
        {

            if (estado != null)
                return _context.TEnvios
                                .Where(e => e.Direccion.Contains(direccion))
                                .Where(e => e.Estado == estado)
                                .Include(e => e.Detalles)
                                    .ThenInclude(p => p.Producto)
                                .ToList();
            return _context.TEnvios
                           .Where(e => e.Direccion.Contains(direccion))
                           .Include(e => e.Detalles)
                                .ThenInclude(p => p.Producto)
                           .ToList();
        }

        public bool Delete(int Id){
            var Envio = _context.TEnvios.Find(Id);
            if(Envio == null || Envio.Estado == "Cancelado"){
                return false;
            }
            Envio.Estado = "Cancelado";
            return _context.SaveChanges() > 0;
        }
    }
}