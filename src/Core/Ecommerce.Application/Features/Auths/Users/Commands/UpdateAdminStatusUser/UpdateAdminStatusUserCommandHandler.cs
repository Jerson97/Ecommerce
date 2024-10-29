using Ecommerce.Application.Exceptions;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace Ecommerce.Application.Features.Auths.Users.Commands.UpdateAdminStatusUser
{
    internal class UpdateAdminStatusUserCommandHandler : IRequestHandler<UpdateAdminStatusUserCommand, User>
    {
        private readonly UserManager<User> _userManager;

        public UpdateAdminStatusUserCommandHandler(UserManager<User> userManager)
        {
            _userManager = userManager;
        }

        public async Task<User> Handle(UpdateAdminStatusUserCommand request, CancellationToken cancellationToken)
        {
            var updateUser = await _userManager.FindByIdAsync(request.Id!);

            if (updateUser is null)
            {
                throw new BadRequestException("El usuario no existe");
            }

            updateUser.isActive = !updateUser.isActive;

            var result = await _userManager.UpdateAsync(updateUser);

            if (!result.Succeeded)
            {
                throw new Exception("No se pudo actualizar el estado del usuario");
            }

            return updateUser;
        }
    }
}
