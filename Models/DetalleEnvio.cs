namespace ModeloParcialApi.Models
{
    public class DetalleEnvio
    {
        public int IdDetalle {get;set;}
        public int IdProducto {get;set;}
        public Producto Producto {get; set;}
        public int Cantidad {get; set;}
        public string Comentario {get; set;}
    }
}