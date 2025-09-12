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
    public class RepositoryFactura : iRepositoryFactura
    {
        private readonly SqlConnection? _connection;
        private readonly SqlTransaction? _transaction;

        public RepositoryFactura(SqlConnection connection = null,
            SqlTransaction transaction = null)
        {
            _connection = connection;
            _transaction = transaction;
        }
        public bool Delete(int id)
        {
            bool resultado = false;
            List<Parametro> parametros = new List<Parametro>()
            {
                new Parametro() { Nombre="@NroFactura", Valor=id }
            };
            if (DataHelper.GetInstance().
                ExecuteSPNonQuery(
                "DeleteFactura", parametros, _connection, _transaction) > 0)
            {
                resultado = true;
            }
            return resultado;
        }

        public List<Factura> GetAll()
        {
            List<Factura> listaArticulos = new List<Factura>();
            var dt = DataHelper.GetInstance().ExecuteSPQuery("GetAllFactura");
            foreach (DataRow dr in dt.Rows)
            {
                Factura factura = new Factura()
                {
                    Fecha = (DateTime)dr["Fecha"],
                    FormaPago = new FormaPago() { IdFormaPago = (int)dr["IdFormaPago"],Nombre = (string)dr["Nombre"] },
                    Cliente = (string)dr["Cliente"],
                    NumeroFactura = (int)dr["NroFactura"]
                };
                listaArticulos.Add(factura);
            }
            return listaArticulos;
        }
        public Factura GetById(int id)
        {
            List<Parametro> parametros = new List<Parametro>()
            {
                new Parametro() { Nombre="@NroFactura", Valor=id }
            };
            DataTable dt = DataHelper.GetInstance().ExecuteSPQuery("GetFacturaById", parametros);
            Factura factura = new Factura();
            if(dt.Rows.Count == 0)
            {
                return null;
            }
            DataRow dr = dt.Rows[0];
            factura.Fecha = (DateTime)dr["Fecha"];
            factura.FormaPago = new FormaPago() { IdFormaPago = (int)dr["IdFormaPago"],Nombre = (string)dr["Nombre"] };
            factura.Cliente = (string)dr["Cliente"];
            factura.NumeroFactura = (int)dr["NroFactura"];
            return factura;
        }

        public int Insert(Factura factura)
        {
            bool resultado = false;
            List<Parametro> parametros = new List<Parametro>()
            {
                new Parametro() { Nombre = "@Fecha", Valor = factura.Fecha },
                new Parametro() { Nombre = "@IdFormaPago", Valor = factura.FormaPago.IdFormaPago },
                new Parametro() { Nombre = "@Cliente", Valor = factura.Cliente }
            };
            int nroFactura = DataHelper.GetInstance().ExecuteSPScalar(
                "InsertFactura", parametros, _connection, _transaction);
            return nroFactura;
        }

        public int Update(Factura factura)
        {
            List<Parametro> parametros = new List<Parametro>()
            {
                new Parametro() { Nombre = "@NroFactura", Valor = factura.NumeroFactura},
                new Parametro() { Nombre = "@Fecha", Valor = factura.Fecha },
                new Parametro() { Nombre = "@IdFormaPago", Valor = factura.FormaPago.IdFormaPago },
                new Parametro() { Nombre = "@Cliente", Valor = factura.Cliente }
            };
            int filas = DataHelper.GetInstance().ExecuteSPNonQuery(
                "UpdateFactura", parametros, _connection, _transaction);
            return filas;
        }
    }
}