using ActividadPractica1_.datos.implementacion;
using ActividadPractica1_.datos.interfaz;
using ActividadPractica1_.dominio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ActividadPractica1_.servicios
{
    public class ServicioArticulo
    {
        private UnitOfWork uow;
        private iRepositoryArticulo _repository;

        public ServicioArticulo()
        {
            _repository = new RepositoryArticulo();
        }
        public bool EliminarArticulo(int id)
        {
            uow = new UnitOfWork();
            bool response = uow.RepositoryArticulo.Delete(id);
            uow.SaveChanges();
            return response;
        }
        public List<Articulo> ObtenerArticulos()
        {
            return _repository.GetAll();
        }
        public Articulo ObtenerArticuloPorId(int id)
        {
            return _repository.GetById(id);
        }
        public bool GuardarArticulo(Articulo articulo)
        {
            uow = new UnitOfWork();
            bool response = uow.RepositoryArticulo.Save(articulo);
            uow.SaveChanges();
            return response;
        }
    }
}