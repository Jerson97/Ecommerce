using Ecommerce.Application.Features.Auths.Users.Vms;
using MediatR;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Application.Features.Auths.Users.Queries.GetUserByUserNameQuery
{
    public class GetUserByUserNameQueryHandler : IRequestHandler<GetUserByUserNameQuery, AuthResponse>
    {
        private readonly UserManager<User> _userManager;

        public GetUserByUserNameQueryHandler(UserManager<User> userManager)
        {
            _userManager = userManager;
        }

        public async Task<AuthResponse> Handle(GetUserByUserNameQuery request, CancellationToken cancellationToken)
        {
            var userName = await _userManager.FindByNameAsync(request.UserName!);

            if (userName is null)
            {
                throw new Exception("El usuario no existe");
            }

            return new AuthResponse
            {
                Id = userName.Id,
                Name = userName.Name,
                LastName = userName.LastName,
                Phone = userName.Phone,
                Email = userName.Email,
                Username = userName.UserName,
                Avatar = userName.AvatarUrl,
                Roles = await _userManager.GetRolesAsync(userName)

            };

        }
    }
}
