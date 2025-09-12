using ActividadPractica1_.dominio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ActividadPractica1_.datos.interfaz;
using ActividadPractica1_.datos.implementacion;

namespace ActividadPractica1_.servicios
{
    public class ServicioFactura : iServicioFactura
    {
        private iRepositoryFactura _repository;
        private iRepositoryDetalleFactura _repositoryDetalles;
        private UnitOfWork uow;
        public ServicioFactura()
        {
            _repository = new RepositoryFactura();
            _repositoryDetalles = new RepositoryDetalleFactura();
        }
        public bool GuardarFactura(Factura factura)
        {
            using (uow = new UnitOfWork()) 
            {
                try
                {
                    int nroFactura = uow.RepositoryFactura.Insert(factura);
                    if (nroFactura <= 0)
                        throw new Exception("No se pudo actualizar la factura.");
                    
                    if (factura.Detalles == null || factura.Detalles.Count < 0)
                        throw new Exception("No se puede guardar una factura sin detalles.");
                    
                    foreach (var detalle in factura.Detalles)
                    {
                        detalle.NroFactura = nroFactura;
                        bool detalleInsertado = uow.RepositoryDetalleFactura.Save(detalle);
                        if (!detalleInsertado)
                        {
                            throw new Exception("Error al ingresar el detalle de la factura.");
                        }
                    }
                    uow.SaveChanges();
                    return true;
                }
                catch (Exception ex) 
                {
                    return false;   
                }
            }
        }
        public bool ActualizarFactura(Factura factura)
        {
            List<DetalleFactura> detallesOriginales = _repositoryDetalles.GetByIdFactura(factura.NumeroFactura);
            using (var uow = new UnitOfWork())
            {
                try
                {
                    int filas = uow.RepositoryFactura.Update(factura);
                    if (filas <= 0)
                        throw new Exception("No se pudo actualizar la factura.");

                    foreach (var detalle in factura.Detalles)
                    {
                        bool resultadoDetalle;
                        if (detalle.IdDetalle == 0)
                        {
                            detalle.NroFactura = factura.NumeroFactura;
                            resultadoDetalle = uow.RepositoryDetalleFactura.Save(detalle);
                        }
                        else
                        {
                            resultadoDetalle = uow.RepositoryDetalleFactura.Update(detalle);
                        }
                        if (!resultadoDetalle)
                        {
                            throw new Exception("Error al actualizar un detalle de la factura.");
                        }
                    }

                    foreach (var detalleGuardado in detallesOriginales)
                    {
                        bool detalleExistente = factura.Detalles.Any(
                            d => d.IdDetalle == detalleGuardado.IdDetalle);
                        if (!detalleExistente)
                        {
                            if (!uow.RepositoryDetalleFactura.Delete(detalleGuardado.IdDetalle))
                                throw new Exception("Error al eliminar un detalle de la factura.");
                        }
                    }
                    uow.SaveChanges();
                    return true;
                }
                catch (Exception ex)
                {
                    return false;
                }
            }
        }
        public List<Factura> ObtenerFacturas()
        {
            List<Factura> facturas = _repository.GetAll();
            foreach (var factura in facturas)
            {
                factura.Detalles = _repositoryDetalles.GetByIdFactura(factura.NumeroFactura);
            }
            return facturas;
        }
        public Factura ObtenerFacturaPorId(int id)
        {   
            Factura facturaEncontrada = _repository.GetById(id);
            if(facturaEncontrada != null)
            {
            facturaEncontrada.Detalles = _repositoryDetalles.GetByIdFactura(id);
            }
            else
            {
                return null;
            }
            return facturaEncontrada;
        }
        public bool EliminarFactura(int id)
        {
            uow = new UnitOfWork();
            bool response = uow.RepositoryFactura.Delete(id);
            uow.SaveChanges();
            return response;
        }
    }
}