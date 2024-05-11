using FluentValidation;
using TarjetaBA.Cliente.Models;

namespace TarjetaBA.Cliente.Validator
{
    public class TarjetaValidator :AbstractValidator<TarjetaViewModel>
    {
        public TarjetaValidator()
        {
            RuleFor(x => x.Cuenta).NotEmpty().WithMessage("la cuenta es obligatorio");
            RuleFor(x => x.Nombre).NotEmpty().WithMessage("El Nombre es obligatorio");
            RuleFor(x => x.Apellido).NotEmpty().WithMessage("El Apellido es obligatorio");
        }
    }
}
