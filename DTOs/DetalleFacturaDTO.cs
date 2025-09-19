using System.ComponentModel.DataAnnotations;

namespace ActividadPractica3.DTOs
{
    public class DetalleFacturaDTO
    {
        public int IdDetalle { get; set; }

        //[Range(1, int.MaxValue, ErrorMessage = "La cantidad debe ser mayor a 0.")]
        public int Cantidad { get; set; }
        public ArticuloDTO Articulo { get; set; }
    }
    public class DetalleFacturaCreateDTO
    {
        public int NroFactura { get; set; }
        public int IdDetalle { get; set; }
        [Range(1, int.MaxValue, ErrorMessage = "La cantidad debe ser mayor a 0.")]
        public int Cantidad { get; set; }
        public int IdArticulo { get; set; }
    }
}
