namespace TarjetaBA.Cliente.Models
{
    public class TransaccionViewModel
    {
        public string Titular { get; set; }
        public string NumeroTarjeta { get; set; }
        public DateTime Fecha { get; set; }
        public string Descripcion { get; set; }
        public decimal Cargo { get; set; }
        public decimal Abono { get; set; }

        // Propiedad que representa el estado de cuenta de una tarjeta
        public EstadoCuentaViewModel EstadoCuenta { get; set; }

        // Colección de pagos asociadas a la tarjeta

        public IEnumerable<CompraViewModel> Compras { get; set; }
        public IEnumerable<PagoViewModel> Pagos { get; set; }
    }
}

