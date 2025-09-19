using ActividadPractica3.Models;

namespace ActividadPractica3.Data.Repositories
{
    public interface IRepositoryFactura
    {
        public Factura? GetById(int id);
        public List<Factura> GetAll();
        public void Insert(Factura factura);
        public void Delete(int id);
        public void Update(Factura factura, int nroFactura);
    }
}
