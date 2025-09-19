using ActividadPractica3.Data.Repositories;
using ActividadPractica3.DTOs;
using ActividadPractica3.Models;
using AutoMapper;

namespace ActividadPractica3.Services
{
    public class ServiceFactura : IServiceFactura
    {
        private readonly IRepositoryFactura _repository;
        private readonly IMapper _mapper;
        public ServiceFactura(IRepositoryFactura repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public void ActualizarFactura(FacturaCreateDTO facturaDto, int nroFactura)
        {
            var factura = _mapper.Map<Factura>(facturaDto);
            _repository.Update(factura, nroFactura);
        }

        public void CrearFactura(FacturaCreateDTO facturaDto)
        {
            var factura = _mapper.Map<Factura>(facturaDto);
            _repository.Insert(factura);
        }

        public void EliminarFactura(int id)
        {
            _repository.Delete(id);
        }

        public List<FacturaDTO> ListarFacturas()
        {
            var facturas = _repository.GetAll();
            return _mapper.Map<List<FacturaDTO>>(facturas);
        }

        public FacturaDTO? ObtenerFactura(int id)
        {
            var factura = _repository.GetById(id);
            return factura == null ? null : _mapper.Map<FacturaDTO>(factura);
        }
    }
}