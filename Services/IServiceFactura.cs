using ActividadPractica3.DTOs;
using ActividadPractica3.Models;

namespace ActividadPractica3.Services
{
    public interface IServiceFactura
    {
        FacturaDTO? ObtenerFactura(int id);
        List<FacturaDTO> ListarFacturas();
        void EliminarFactura(int id);
        void CrearFactura(FacturaCreateDTO factura);
        void ActualizarFactura(FacturaCreateDTO factura, int nroFactura);
    }
}
