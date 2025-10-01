using ActividadPractica3.DTOs;
using ActividadPractica3.Models;

namespace ActividadPractica3.Services
{
    public interface IServiceFactura
    {
        Task<FacturaDTO?> ObtenerFactura(int id);
        Task<List<FacturaDTO>> ListarFacturas();
        Task EliminarFactura(int id);
        Task CrearFactura(FacturaCreateDTO factura);
        Task ActualizarFactura(FacturaCreateDTO factura, int nroFactura);
    }
}
