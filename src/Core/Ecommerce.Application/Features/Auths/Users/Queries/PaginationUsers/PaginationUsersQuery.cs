using Ecommerce.Application.Features.Shared.Queries;
using MediatR;

namespace Ecommerce.Application.Features.Auths.Users.Queries.PaginationUsers
{
    public class PaginationUsersQuery : PaginationBaseQuery, IRequest<PaginationVm<User>>
    {

    }
}
