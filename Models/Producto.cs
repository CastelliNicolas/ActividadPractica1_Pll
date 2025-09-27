namespace ModeloParcialApi.Models
{
    public class Producto
    {
        public int IdProducto {get; set;}
        public string Nombre {get; set;}
        public decimal Precio {get; set;}
        public ICollection<DetalleEnvio> Detalles{get; set;}  = new List<DetalleEnvio>();
    }
}