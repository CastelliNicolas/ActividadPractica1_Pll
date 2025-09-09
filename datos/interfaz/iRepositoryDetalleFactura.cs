using ActividadPractica1_.dominio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ActividadPractica1_.datos.interfaz
{
    public interface iRepositoryDetalleFactura
    {
        List<DetalleFactura> GetAll();

        List<DetalleFactura> GetByIdFactura(int id);

        bool Save(DetalleFactura detalle);
        bool Update(DetalleFactura detalle);

        bool Delete(int id);
    }
}
