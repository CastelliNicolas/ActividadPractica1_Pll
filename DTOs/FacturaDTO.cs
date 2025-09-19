namespace ActividadPractica3.DTOs
{
    public class FacturaDTO
    {
        public int NroFactura { get; set; }
        public string Cliente { get; set; }
        public DateOnly Fecha { get; set; }
        public int IdFormaPago { get; set; }
        public FormaPagoDTO FormaPago { get; set; }
        public List<DetalleFacturaDTO> DetalleFacturas { get; set; }
    }

    public class FacturaCreateDTO
    {
        public int NroFactura { get; set; }
        public string Cliente { get; set; }
        public DateOnly Fecha { get; set; }
        public int IdFormaPago { get; set; }
        public List<DetalleFacturaCreateDTO> DetalleFacturas { get; set; } = new();
    }
}
