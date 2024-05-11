namespace TarjetaBA.Server.Models
{
    public class Transaccion
    {
        //Se crea los campos de la tabñla realacional
        public string Titular { get; set; }
        public int NumeroTarjeta { get; set; }
        public DateTime Fecha { get; set; }
        public string Descripcion { get; set; }
        public decimal Cargo { get; set; }
        public decimal Abono { get; set; }
    }
}
