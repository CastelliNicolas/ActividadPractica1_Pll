using ActividadPractica1_.datos.helpers;
using ActividadPractica1_.datos.interfaz;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ActividadPractica1_.datos.implementacion
{
    public class UnitOfWork : IDisposable
    {
        private readonly SqlConnection _connection;
        private SqlTransaction? _transaction;
        private iRepositoryFactura _repositoryFactura;
        private iRepositoryDetalleFactura _repositoryDetalleFactura;
        private iRepositoryArticulo _repositoryArticulos;

        public UnitOfWork()
        {
            _connection = DataHelper.GetInstance().GetConnection();
            _connection.Open();
            _transaction = _connection.BeginTransaction();
        }
        public iRepositoryFactura RepositoryFactura
        {
            get
            {
                if (_repositoryFactura == null)
                {
                    _repositoryFactura = new RepositoryFactura(_connection, _transaction);
                }
                return _repositoryFactura;
            }
        }
        public iRepositoryDetalleFactura RepositoryDetalleFactura
        {
            get
            {
                if (_repositoryDetalleFactura == null)
                {
                    _repositoryDetalleFactura = new RepositoryDetalleFactura(_connection, _transaction);
                }
                return _repositoryDetalleFactura;
            }
        }
        public iRepositoryArticulo RepositoryArticulo
        {
            get
            {
                if (_repositoryArticulos == null)
                {
                    _repositoryArticulos = new RepositoryArticulo(_connection, _transaction);
                }
                return _repositoryArticulos;
            }
        }
        public void SaveChanges()
        {
            try
            {
                _transaction.Commit();
            }
            catch (Exception ex)
            {
                _transaction.Rollback();
                throw new Exception("Error al guardar cambios en la base de datos.", ex);
            }
        }
        public void Dispose()
        {
            if (_transaction != null)
            {
                _transaction.Dispose();
            }
            if (_connection != null)
            {
                _connection.Close();
                _connection.Dispose();
            }
        }
    }
}