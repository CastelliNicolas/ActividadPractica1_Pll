using ActividadPractica3.Data.Repositories;
using ActividadPractica3.DTOs;
using ActividadPractica3.Models;
using AutoMapper;
using System.Runtime.CompilerServices;

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

        public async Task ActualizarFactura(FacturaCreateDTO facturaDto, int nroFactura)
        {
            var factura = _mapper.Map<Factura>(facturaDto);
            await _repository.Update(factura, nroFactura);
        }

        public async Task CrearFactura(FacturaCreateDTO facturaDto)
        {
            var factura = _mapper.Map<Factura>(facturaDto);
            await _repository.Insert(factura);
        }

        public async Task EliminarFactura(int id)
        {
            await _repository.Delete(id);
        }

        public async Task<List<FacturaDTO>> ListarFacturas()
        {
            var facturas = await _repository.GetAll();
            return _mapper.Map<List<FacturaDTO>>(facturas);
        }

        public async Task<FacturaDTO?> ObtenerFactura(int id)
        {
            var factura = await _repository.GetById(id);
            return factura == null ? null : _mapper.Map<FacturaDTO>(factura);
        }
    }
}