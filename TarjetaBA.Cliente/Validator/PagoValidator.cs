using FluentValidation;
using TarjetaBA.Cliente.Models;

namespace TarjetaBA.Cliente.Validator
{
    public class PagoValidator: AbstractValidator<PagoViewModel>
    {
        public PagoValidator() 
        {
            RuleFor(x => x.Fecha).NotEmpty().WithMessage("La fecha es obligatorio");
            RuleFor(x => x.Abono).NotEmpty().WithMessage("El Abono es obligatorio");
        }
    }
}
