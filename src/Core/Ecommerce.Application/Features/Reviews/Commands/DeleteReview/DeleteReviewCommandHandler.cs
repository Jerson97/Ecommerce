using AutoMapper;
using Ecommerce.Application.Exceptions;
using Ecommerce.Application.Persistence;
using Ecommerce.Domain;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Application.Features.Reviews.Commands.DeleteReview
{
    public class DeleteReviewCommandHandler : IRequestHandler<DeleteReviewCommand>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public DeleteReviewCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<Unit> Handle(DeleteReviewCommand request, CancellationToken cancellationToken)
        {
            var reviewToDelte = await _unitOfWork.Repository<Review>().GetByIdAsync(request.ReviewId);

            if (reviewToDelte is null)
            {
                throw new NotFoundException(nameof(Review), request.ReviewId);
            }

            _unitOfWork.Repository<Review>().DeleteEntity(reviewToDelte);
            await _unitOfWork.Complete();

            return Unit.Value;
        }
    }
}
