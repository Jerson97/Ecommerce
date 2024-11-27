using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Application.Features.Addresses.Commands.CreateAddress
{
    public class CreateAddressCommandValidator : AbstractValidator<CreateAddressCommand>
    {
        public CreateAddressCommandValidator()
        {
            RuleFor(p => p.StreetAddress)
                .NotEmpty().WithMessage("La direccion no puede ser nula");

            RuleFor(p => p.City)
                .NotEmpty().WithMessage("La ciudad no puede ser nula");

            RuleFor(p => p.Department)
                .NotEmpty().WithMessage("El departamento no puede ser nula");

            RuleFor(p => p.PostalCode)
                .NotEmpty().WithMessage("El codigo postal no puede ser nula");

            RuleFor(p => p.Country)
                .NotEmpty().WithMessage("El pais no puede ser nula");
        }
    }
}
