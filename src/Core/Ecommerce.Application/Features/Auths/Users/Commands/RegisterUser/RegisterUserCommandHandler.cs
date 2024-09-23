using AutoMapper;
using Ecommerce.Application.Contracts.Identity;
using Ecommerce.Application.Exceptions;
using Ecommerce.Application.Features.Auths.Users.Vms;
using Ecommerce.Application.Persistence;
using Ecommerce.Domain;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace Ecommerce.Application.Features.Auths.Users.Commands.RegisterUser
{
    public class RegisterUserCommandHandler : IRequestHandler<RegisterUserCommand, AuthResponse>
    {
        private readonly UserManager<User> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IAuthService _authService;

        public RegisterUserCommandHandler(UserManager<User> userManager, RoleManager<IdentityRole> roleManager, IAuthService authService)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _authService = authService;
        }

        public async Task<AuthResponse> Handle(RegisterUserCommand request, CancellationToken cancellationToken)
        {
            var email = await _userManager.FindByEmailAsync(request.Email!) is null ? false : true;

            if (email)
            {
                throw new BadRequestException("El Email del usuario ya existe en la base de datos");
            }

            var userName = await _userManager.FindByNameAsync(request.UserName!) is null ? false : true;

            if (userName)
            {
                throw new BadRequestException("El UserName del usuario ya existe en la base de datos");
            }

            var user = new User
            {
                Name = request.Name,
                LastName = request.LastName,
                Phone = request.Phone,
                Email = request.Email,
                UserName = request.UserName,
                AvatarUrl = request.PhotoUrl
            };

            var result = await _userManager.CreateAsync(user!, request.Password!);

            if (result.Succeeded)
            {
                await _userManager.AddToRoleAsync(user, AppRole.GenericUser);

                var roles = await _userManager.GetRolesAsync(user);

                var rolesList = new List<string>(roles);

                return new AuthResponse
                {
                    Id = user.Id,
                    Name = user.Name,
                    LastName = user.LastName,
                    Phone = user.Phone,
                    Username = user.UserName,
                    Email = user.Email,
                    Avatar = user.AvatarUrl,
                    Token = _authService.CreateToken(user, rolesList),
                    Roles = roles
                };
            }

            throw new Exception("No se pudo registrar el usuario");
        }
    }
}
