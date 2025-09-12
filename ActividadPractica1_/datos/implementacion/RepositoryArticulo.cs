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
    public class RepositoryArticulo : iRepositoryArticulo
    {
        private readonly SqlConnection _connection;
        private readonly SqlTransaction _transaction;

        public RepositoryArticulo(SqlConnection? connection = null,
            SqlTransaction? transaction = null)
        {
            _connection = connection;
            _transaction = transaction;
        }
        public List<Articulo> GetAll()
        {
            List<Articulo> listaArticulos = new List<Articulo>();
            var dt = DataHelper.GetInstance().ExecuteSPQuery("GetAllArticulo");
            foreach(DataRow dr in dt.Rows){
                Articulo articulo = new Articulo()
                {
                    IdArticulo = (int)dr["IdArticulo"],
                    Nombre = (string)dr["Nombre"],
                    Precio = (decimal)dr["PrecioUnitario"],
                };
                listaArticulos.Add(articulo);
            }
            return listaArticulos;
        }
        public bool Delete(int id)
        {
            bool resultado = false;
            List<Parametro> parametros = new List<Parametro>()
            {
                new Parametro() { Nombre="@IdArticulo", Valor=id }
            };
            if (DataHelper.GetInstance().ExecuteSPNonQuery("DeleteArticulo", parametros, _connection, _transaction) > 0)
            {
                resultado = true;
            }
            return resultado;
        }
        public Articulo GetById(int id)
        {
            List<Parametro> parametros = new List<Parametro>()
            {
                new Parametro() { Nombre="@IdArticulo", Valor=id }
            };
            DataTable dt = DataHelper.GetInstance().ExecuteSPQuery("GetArticuloById", parametros);
            Articulo articulo = new Articulo();

            DataRow dr = dt.Rows[0];
            articulo.IdArticulo = id;
            articulo.Nombre = (string)dr["Nombre"];
            articulo.Precio = (decimal)dr["PrecioUnitario"];
            return articulo;
        }

        public bool Save(Articulo articulo)
        {
            List<Parametro> parametros = new List<Parametro>();
            bool resultado = false;
            string sp;
            if (articulo.IdArticulo > 0)
            {
                sp = "UpdateArticulo";
                parametros.Add(new Parametro() { Nombre = "@IdArticulo", Valor = articulo.IdArticulo } );
                parametros.Add(new Parametro() { Nombre = "@Nombre", Valor = articulo.Nombre } );
                parametros.Add(new Parametro() { Nombre = "@PrecioUnitario", Valor = articulo.Precio } );
            }
            else
            {
                sp = "InsertArticulo";
                parametros.Add(new Parametro() { Nombre = "@Nombre", Valor = articulo.Nombre });
                parametros.Add(new Parametro() { Nombre = "@PrecioUnitario", Valor = articulo.Precio });
            }
            if(DataHelper.GetInstance().ExecuteSPNonQuery(sp, parametros, _connection, _transaction) > 0)
            {
                resultado = true;
            }
            return resultado;
        }
    }
}