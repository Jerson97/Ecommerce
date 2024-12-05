using Ecommerce.Application.Features.Orders.Vms;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Application.Features.Orders.Commands.UpdateOrder
{
    public class UpdateOrderCommand : IRequest<OrderVm>
    {
        public int OrderId { get; set; }
        public OrderStatus Status { get; set; }
    }
}
