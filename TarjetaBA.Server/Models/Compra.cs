using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using TarjetaBA.Server.Models;

namespace TarjetaBA.Server.Models
{
    public partial class Compra
    {


        public int IdCompra { get; set; }
        public int? IdTarjeta { get; set; }

        public int Cuenta { get; set; }
        public string? Nombre { get; set; }
        public string? Apellido { get; set; }
        public DateTime Fecha { get; set; }
        public string Descripcion { get; set; }
        public decimal Monto { get; set; }

        //Hace que se nula y ingnora json shemas
        public virtual Tarjeta? oTarjeta { get; set; }
        [JsonIgnore]
        public virtual ICollection<Pago>? Pagos { get; set; }

    }
}
