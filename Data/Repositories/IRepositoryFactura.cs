using ActividadPractica3.Models;

namespace ActividadPractica3.Data.Repositories
{
    public interface IRepositoryFactura
    {
        public Task<Factura?> GetById(int id);
        public Task<List<Factura>> GetAll();
        public Task Insert(Factura factura);
        public Task Delete(int id);
        public Task Update(Factura factura, int nroFactura);
    }
}
