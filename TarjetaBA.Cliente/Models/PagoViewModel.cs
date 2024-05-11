using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace TarjetaBA.Cliente.Models
{
    public class PagoViewModel
    {
        //public int Id { get; set; }
        public DateTime Fecha { get; set; }
        public decimal Abono { get; set; }
    }
}