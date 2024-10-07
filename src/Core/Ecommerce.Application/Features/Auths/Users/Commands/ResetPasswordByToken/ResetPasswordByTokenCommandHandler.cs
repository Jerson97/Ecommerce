using Ecommerce.Application.Exceptions;
using MediatR;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Application.Features.Auths.Users.Commands.ResetPasswordByToken
{
    public class ResetPasswordByTokenCommandHandler : IRequestHandler<ResetPasswordByTokenCommand, string>
    {
        private readonly UserManager<User> _userManager;

        public ResetPasswordByTokenCommandHandler(UserManager<User> userManager)
        {
            _userManager = userManager;
        }

        public async Task<string> Handle(ResetPasswordByTokenCommand request, CancellationToken cancellationToken)
        {
            if(!string.Equals(request.Password, request.ConfirmPassword))
            {
                throw new BadRequestException("El password no es igual a la configuaracion del password");
            }

            var updateUser = await _userManager.FindByEmailAsync(request.Email!);
            if(updateUser is null)
            {
                throw new BadRequestException("El email no esta registrado como usuario");
            }

            var token = Convert.FromBase64String(request.Token!);
            var tokenResult = Encoding.UTF8.GetString(token);

            var resetResult = await _userManager.ResetPasswordAsync(updateUser, tokenResult, request.Password!);
            if (!resetResult.Succeeded)
            {
                throw new Exception("No se pudo resetear el password");
            }

            return $"Se actualizo exitosamente tu password ${request.Email}";

        }
    }
}
