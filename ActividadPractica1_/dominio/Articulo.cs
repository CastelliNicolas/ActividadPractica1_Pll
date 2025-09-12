using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ActividadPractica1_.dominio
{
    public class Articulo
    {
        public int IdArticulo { get; set; }
        public string Nombre { get; set; }
        public decimal Precio { get; set; }

        public Articulo(int Id, string Nombre, decimal Precio) 
        {
            this.IdArticulo = Id;
            this.Nombre = Nombre;
            this.Precio = Precio;
        }
        public Articulo() 
        {
            this.IdArticulo = 0;
            this.Nombre = "";
            this.Precio = 0;
        }
        public override string ToString()
        {
            return "Nombre: " + Nombre + " Precio: " + Precio;
        }
    }
}