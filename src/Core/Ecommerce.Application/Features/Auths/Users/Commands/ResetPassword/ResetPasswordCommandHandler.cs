using Ecommerce.Application.Contracts.Identity;
using Ecommerce.Application.Exceptions;
using MediatR;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Application.Features.Auths.Users.Commands.ResetPassword
{
    public class ResetPasswordCommandHandler : IRequestHandler<ResetPasswordCommand>
    {
        private readonly UserManager<User> _userManager;
        private readonly IAuthService _authService;

        public ResetPasswordCommandHandler(UserManager<User> userManager, IAuthService authService)
        {
            _userManager = userManager;
            _authService = authService;
        }

        public async Task<Unit> Handle(ResetPasswordCommand request, CancellationToken cancellationToken)
        {
            var updateUser = await _userManager.FindByNameAsync(_authService.GetSessionUser());
            if (updateUser is null) 
            {
                throw new BadRequestException("El usuario no existe");
            }

            var resultValidateOldPassword = _userManager.PasswordHasher.VerifyHashedPassword(updateUser, updateUser.PasswordHash!, request.OldPassword!);

            if (!(resultValidateOldPassword == PasswordVerificationResult.Success))
            {
                throw new BadRequestException("El actual password ingresado es erroneo");
            }

            var hashedNewPassword = _userManager.PasswordHasher.HashPassword(updateUser, request.NewPassword!);
            updateUser.PasswordHash = hashedNewPassword;

            var result = await _userManager.UpdateAsync(updateUser);

            if (!result.Succeeded)
            {
                throw new Exception("No se pudo resetear el password");
            }

            return Unit.Value;
        }
    }
}
