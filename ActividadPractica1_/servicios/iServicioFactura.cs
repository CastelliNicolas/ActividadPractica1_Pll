using ActividadPractica1_.dominio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ActividadPractica1_.servicios
{
    public interface iServicioFactura
    {
        public bool GuardarFactura(Factura factura);
        public bool ActualizarFactura(Factura factura);
        public List<Factura> ObtenerFacturas();
        public Factura ObtenerFacturaPorId(int id);
        public bool EliminarFactura(int id);
    }
}
