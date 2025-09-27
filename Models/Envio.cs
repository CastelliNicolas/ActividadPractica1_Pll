namespace ModeloParcialApi.Models
{
    public class Envio
    {
        public int Id {get; set;}
        public DateTime Fecha {get; set;}
        public int DniCliente {get; set;}
        public string Direccion {get; set;}
        public string PalabraSecreta {get; set;}
        public string Estado {get; set;}
        public List<DetalleEnvio> Detalles {get; set;}
    }
}