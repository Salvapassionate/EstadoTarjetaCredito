namespace TarjetaBA.Server.Models
{
    public class EstadoCuenta
    {
        public string Titular { get; set; }
        public int NumeroTarjeta { get; set; }
        public decimal SaldoActual { get; set; }
        public decimal LimiteCredito { get; set; }
        public decimal SaldoDisponible { get; set; }
        public decimal MontoTotalMesActual { get; set; }
        public decimal MontoTotalMesAnterior { get; set; }
        public decimal InteresBonificable { get; set; }
        public decimal CuotaMinimaPagar { get; set; }
        public decimal MontoTotalPagar { get; set; }
        public decimal MontoTotalContadoIntereses { get; set; }
        public DateTime Fecha { get; set; }
        public string Descripcion { get; set; }
        public decimal Monto { get; set; }
    }
}
