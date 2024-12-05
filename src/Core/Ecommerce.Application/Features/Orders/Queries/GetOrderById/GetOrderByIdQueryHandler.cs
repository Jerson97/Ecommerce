using AutoMapper;
using Ecommerce.Application.Features.Orders.Vms;
using Ecommerce.Application.Persistence;
using Ecommerce.Domain;
using MediatR;
using System.Linq.Expressions;

namespace Ecommerce.Application.Features.Orders.Queries.GetOrderById
{
    public class GetOrderByIdQueryHandler : IRequestHandler<GetOrderByIdQuery, OrderVm>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetOrderByIdQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<OrderVm> Handle(GetOrderByIdQuery request, CancellationToken cancellationToken)
        {
            var includes = new List<Expression<Func<Order, object>>>();
            includes.Add(p => p.OrderItems!.OrderBy(x => x.Product));
            includes.Add(p => p.OrderAddres!);

            var order = await _unitOfWork.Repository<Order>().GetEntityAsync(
                x => x.Id == request.OrderId,
                includes,
                false
            );

            return _mapper.Map<OrderVm>(order);
        }
    }
}
