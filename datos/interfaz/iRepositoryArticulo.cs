using ActividadPractica1_.dominio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ActividadPractica1_.datos.interfaz
{
    public interface iRepositoryArticulo
    {
        List<Articulo> GetAll();

        Articulo GetById(int id);

        bool Save(Articulo articulo);

        bool Delete(int id);
    }
}
