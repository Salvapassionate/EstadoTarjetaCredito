using System.ComponentModel.DataAnnotations;
using TarjetaBA.Server.Models;

namespace TarjetaBA.Server.Models
{
    public class Pago
    {
        public int IdPago { get; set; }
        public int IdCompra { get; set; }
        public int IdTarjeta { get; set; }

        public string? Cuenta { get; set; }
        public string? Nombre { get; set; }
        public string? Apellido { get; set; }

        public DateTime Fecha { get; set; }

        public string Descripcion { get; set; }
        public decimal Monto { get; set; }

        public decimal Abono { get; set; }

        //el ? hacer que no se nula 
        public virtual Compra? oCompra { get; set; }
    }
}

