using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ActividadPractica1_.datos.helpers
{
    public class DataHelper
    {
        private static DataHelper _instance;
        private readonly string _connectionString;

        private DataHelper()
        {
            _connectionString = Properties.Resources.CadenaConexion;
        }

        public SqlConnection GetConnection()
        {
            return new SqlConnection(_connectionString);
        }

        public static DataHelper GetInstance()
        {
            if (_instance == null)
            {
                _instance = new DataHelper();
            }
            return _instance;
        }

        public DataTable ExecuteSPQuery(string sp, List<Parametro>? parametro = null)
        {
            DataTable dt = new DataTable();

            using (var connection = new SqlConnection(_connectionString))
            {
                try
                {
                    connection.Open();
                    var cmd = new SqlCommand(sp, connection);
                    cmd.CommandType = CommandType.StoredProcedure;

                    if (parametro != null)
                    {
                        foreach (Parametro p in parametro)
                        {
                            cmd.Parameters.AddWithValue(p.Nombre, p.Valor);
                        }
                    }

                    using (var reader = cmd.ExecuteReader())
                    {
                        dt.Load(reader);
                    }
                }
                catch (Exception ex)
                {
                    dt = null;
                    throw; 
                }
            } 

            return dt;
        }

        
        public int ExecuteSPNonQuery(string sp, List<Parametro> parametros, SqlConnection cnn, SqlTransaction t)
        {
            int filasAfectadas = 0;
            try
            {
                var cmd = new SqlCommand(sp, cnn, t);
                cmd.CommandType = CommandType.StoredProcedure;
                foreach (Parametro p in parametros)
                {
                    cmd.Parameters.AddWithValue(p.Nombre, p.Valor);
                }
                filasAfectadas = cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                filasAfectadas = -1;
                throw; 
            }
            return filasAfectadas;
        }

        public int ExecuteSPScalar(string sp, List<Parametro> parametros, SqlConnection cnn, SqlTransaction t)
        {
            int resultado = 0;
            try
            {
                var cmd = new SqlCommand(sp, cnn, t);
                cmd.CommandType = CommandType.StoredProcedure;
                foreach (Parametro p in parametros)
                {
                    cmd.Parameters.AddWithValue(p.Nombre, p.Valor);
                }
                resultado = Convert.ToInt32(cmd.ExecuteScalar());
            }
            catch (Exception ex)
            {
                resultado = -1;
                throw; 
            }
            return resultado;
        }
    }
}