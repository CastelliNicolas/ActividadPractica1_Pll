using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ActividadPractica1_.dominio
{
    public class Factura
    {
        public int NumeroFactura { get; set; }
        public DateTime Fecha { get; set; }
        public FormaPago FormaPago { get; set; }
        public string Cliente { get; set; }
        public List<DetalleFactura> Detalles { get; set; }
        public override string ToString()
        {
            return $"Factura N°: {NumeroFactura}, " +
                    $"Fecha: {Fecha.ToShortDateString()}, " +
                    $"Forma de Pago: {FormaPago.Nombre}, " +
                    $"Cliente: {Cliente}";
        }
    }
}