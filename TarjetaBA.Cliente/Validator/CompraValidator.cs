using FluentValidation;
using TarjetaBA.Cliente.Models;

namespace TarjetaBA.Cliente.Validator
{
    public class CompraValidator : AbstractValidator<CompraViewModel>
    {
        public CompraValidator()
        {
            RuleFor(x => x.Fecha).NotEmpty().WithMessage("La fecha es obligatorio");
            RuleFor(x => x.Descripcion).NotEmpty().WithMessage("La descripcion es obligatorio");
            RuleFor(x => x.Monto).NotEmpty().WithMessage("El monto es obligatorio");

        }
    }
}
