using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace TarjetaBA.Server.Models
{
    public partial class Tarjeta
    {

        public int IdTarjeta { get; set; }
        public int Cuenta { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }

        /*[JsonIgnore]
        public virtual Compra? oCompra { get; set; }
       */
        //Oclta el Json Shemas de Compra
        [JsonIgnore]
        public virtual ICollection<Compra>? Compras { get; set; }
    }

}
