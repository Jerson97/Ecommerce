using Ecommerce.Application.Contracts.Identity;
using Ecommerce.Application.Exceptions;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace Ecommerce.Application.Features.Auths.Users.Commands.UpdateAdminUser
{
    public class UpdateAdminUserCommandHandler : IRequestHandler<UpdateAdminUserCommand, User>
    {
        private readonly UserManager<User> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IAuthService _authService;

        public UpdateAdminUserCommandHandler(UserManager<User> userManager, RoleManager<IdentityRole> roleManager, IAuthService authService)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _authService = authService;
        }

        public async Task<User> Handle(UpdateAdminUserCommand request, CancellationToken cancellationToken)
        {
            var updateUser = await _userManager.FindByIdAsync(request.Id!);
            if (updateUser is null)
            {
                throw new BadRequestException("El usuario no existe");
            }

            updateUser.Name = request.Name;
            updateUser.LastName = request.LastName;
            updateUser.Phone = request.Phone;

            var result = await _userManager.UpdateAsync(updateUser);

            if (!result.Succeeded)
            {
                throw new Exception("No se pudo actualizar el usuario");
            }

            var role = await _roleManager.FindByNameAsync(request.Role!);
            if (role is null)
            {
                throw new Exception("El Rol asignado no existe");
            }

            await _userManager.AddToRoleAsync(updateUser, role.Name!);

            return updateUser;
        }
    }
}
