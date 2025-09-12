using ActividadPractica1_.datos.helpers;
using ActividadPractica1_.datos.interfaz;
using ActividadPractica1_.dominio;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ActividadPractica1_.datos.implementacion
{
    internal class RepositoryDetalleFactura : iRepositoryDetalleFactura
    {
        private readonly SqlConnection _connection;
        private readonly SqlTransaction? _transaction;

        public RepositoryDetalleFactura(SqlConnection? connection = null,
            SqlTransaction? transaction = null)
        {
            _connection = connection;
            _transaction = transaction;
        }
        public bool Delete(int id)
        {
            bool resultado = false;
            List<Parametro> parametros = new List<Parametro>()
            {
                new Parametro() { Nombre="@IdDetalle", Valor=id }
            };
            if (DataHelper.GetInstance().
                ExecuteSPNonQuery(
                "DeleteDetalleFactura", parametros, _connection, _transaction) > 0)
            {
                resultado = true;
            }
            return resultado;
        }

        public List<DetalleFactura> GetAll()
        {
            throw new NotImplementedException();
        }

        public List<DetalleFactura> GetByIdFactura(int id)
        {
            List<DetalleFactura> detalles = new List<DetalleFactura>();
            List<Parametro> parametros = new List<Parametro>()
            {
                new Parametro() { Nombre="@NroFactura", Valor=id }
            };
            DataTable dt = DataHelper.GetInstance().ExecuteSPQuery(
                "GetDetalleFacturaByFactura", parametros);
            foreach (DataRow dr in dt.Rows)
            {
                DetalleFactura detalle = new DetalleFactura()
                {
                    Articulo = new Articulo()
                    {
                        IdArticulo = (int)dr["IdArticulo"],
                        Nombre = (string)dr["Nombre"],
                        Precio = (decimal)dr["PrecioUnitario"]
                    },
                    Cantidad = (int)dr["Cantidad"],
                    IdDetalle = (int)dr["IdDetalle"],
                    NroFactura = id
                };
                detalles.Add(detalle);
            }
            return detalles;
        }

        public bool Save(DetalleFactura detalle)
        {
            bool resultado = false;
            List<Parametro> parametros = new List<Parametro>
            {
                new Parametro(){Nombre="@NroFactura", Valor=detalle.NroFactura},
                new Parametro(){Nombre="@Cantidad", Valor=detalle.Cantidad},
                new Parametro(){Nombre="@IdArticulo", Valor=detalle.Articulo.IdArticulo}
            };
            int filas = DataHelper.GetInstance().ExecuteSPNonQuery(
                "InsertDetalleFactura", parametros, _connection, _transaction);
            if(filas > 0)
            {
                resultado = true;
            }
            return resultado;
        }
        public bool Update(DetalleFactura detalle)
        {
            bool resultado = false;
            List<Parametro> parametros = new List<Parametro>
            {
                new (){Nombre="IdDetalle", Valor =detalle.IdDetalle},
                new (){Nombre="@NroFactura", Valor=detalle.NroFactura},
                new (){Nombre="@Cantidad", Valor=detalle.Cantidad},
                new (){Nombre="@IdArticulo", Valor=detalle.Articulo.IdArticulo}
            };
            int filas = DataHelper.GetInstance().ExecuteSPNonQuery(
                "UpdateDetalleFactura", parametros, _connection, _transaction);
            if (filas > 0)
            {
                resultado = true;
            }
            return resultado;
        }
    }
}