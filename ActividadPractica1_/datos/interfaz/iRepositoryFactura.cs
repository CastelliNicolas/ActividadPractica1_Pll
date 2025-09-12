using ActividadPractica1_.dominio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ActividadPractica1_.datos.interfaz
{
    public interface iRepositoryFactura
    {
        List<Factura>GetAll();
        Factura GetById(int id);
        int Insert(Factura factura);
        int Update(Factura factura);
        bool Delete(int id);
    }
}
