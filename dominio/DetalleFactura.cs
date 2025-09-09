using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ActividadPractica1_.dominio
{
    public class DetalleFactura
    {
        public int IdDetalle { get; set; }
        public int NroFactura { get; set; }
        public Articulo Articulo { get; set; }
        public int Cantidad { get; set; }

        public override string ToString()
        {
            return $"ID:{IdDetalle} - {Articulo} x {Cantidad} unidades";
        }
    }
}