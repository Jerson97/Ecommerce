using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Application.Features.Reviews.Commands.CreateReview
{
    public class CreateReviewCommandValidator : AbstractValidator<CreateReviewCommand>
    {
        public CreateReviewCommandValidator()
        {
            RuleFor(p => p.Name)
                .NotNull().WithMessage("Nombre no permite valores nulos");

            RuleFor(p => p.Comment)
                .NotNull().WithMessage("Comentario no permite valores nulos");

            RuleFor(p => p.Rating)
                .NotEmpty().WithMessage("Rating no permite valores nulos");
        }
    }
}
