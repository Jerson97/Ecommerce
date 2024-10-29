using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Application.Features.Auths.Users.Commands.UpdateAdminUser
{
    public class UpdateAdminUserCommandValidator : AbstractValidator<UpdateAdminUserCommand>
    {
        public UpdateAdminUserCommandValidator()
        {
            RuleFor(x => x.Name).NotEmpty().WithMessage("El nombre no puede estar vacio");
            RuleFor(x => x.LastName).NotEmpty().WithMessage("El apellido no puede estar vacio");
            RuleFor(x => x.Phone).NotEmpty().WithMessage("El telefono no puede estar vacio");
        }
    }
}
