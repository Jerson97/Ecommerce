using Ecommerce.Application.Contracts.Identity;
using Ecommerce.Application.Exceptions;
using Ecommerce.Application.Features.Auths.Users.Vms;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace Ecommerce.Application.Features.Auths.Users.Commands.UpdateUser
{
    public class UpdateUserCommandHandler : IRequestHandler<UpdateUserCommand, AuthResponse>
    {
        private readonly UserManager<User> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IAuthService _authService;

        public UpdateUserCommandHandler(UserManager<User> userManager, RoleManager<IdentityRole> roleManager, IAuthService authService)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _authService = authService;
        }

        public async Task<AuthResponse> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
        {
            var updateUser = await _userManager.FindByNameAsync(_authService.GetSessionUser());
            if (updateUser is null) 
            {
                throw new BadRequestException("El usuario no existe");
            }

            updateUser.Name = request.Name;
            updateUser.LastName = request.LastName;
            updateUser.Phone = request.Phone;
            updateUser.AvatarUrl = request.PhotoUrl ?? updateUser.AvatarUrl;
            //updateUser.Email = request.Email ?? updateUser.Email;

            var result = await _userManager.UpdateAsync(updateUser);

            if (!result.Succeeded) 
            {
                throw new Exception("No se pudo actualizar el usuario");
            }

            var userById = await _userManager.FindByEmailAsync(request.Email!);
            var roles = await _userManager.GetRolesAsync(userById!);
            var listRole = new List<string>(roles);

            return new AuthResponse
            {
                Id = userById!.Id,
                Name = request.Name,
                LastName = request.LastName,
                Phone = request.Phone,
                Email = request.Email,
                Username = userById.Name,
                Avatar = userById.AvatarUrl,
                Token = _authService.CreateToken(userById, listRole),
                Roles = roles
            };
        }
    }
}
