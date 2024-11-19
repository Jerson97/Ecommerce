using Ecommerce.Application.Features.Reviews.Queries.Vms;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Application.Features.Reviews.Commands.CreateReview
{
    public class CreateReviewCommand : IRequest<ReviewVm>
    {
        public int ProductId { get; set; }
        public string? Name { get; set; }
        public int Rating { get; set; }
        public string? Comment { get; set; }
    }
}
