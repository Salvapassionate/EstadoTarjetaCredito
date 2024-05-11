using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace TarjetaBA.Cliente.Models
{
    public class TarjetaViewModel
    {


        public int IdTarjeta { get; set; }
        public string Cuenta { get; set; }

        //Se valida los datos

        [DataType(DataType.MultilineText)]
        [MaxLength(50, ErrorMessage = "EL campo {0} debe tener máximo {1} caracteres")]
        public string Nombre { get; set; } = null!;

        [DataType(DataType.MultilineText)]
        [MaxLength(50, ErrorMessage = "EL campo {0} debe tener máximo {1} caracteres")]
        public string Apellido { get; set; } = null!;

    }
}
