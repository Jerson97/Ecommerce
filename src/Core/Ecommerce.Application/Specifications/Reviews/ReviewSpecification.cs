using Ecommerce.Domain;

namespace Ecommerce.Application.Specifications.Reviews
{
    public class ReviewSpecification : BaseSpecification<Review>
    {
        public ReviewSpecification(ReviewSpecificationParam reviewParams)
            : base(
                  x => (!reviewParams.ProductoId.HasValue || x.ProductId == reviewParams.ProductoId))
        {
            ApplyPagin(reviewParams.PageSize * (reviewParams.PageIndex - 1), reviewParams.PageSize);

            if (!string.IsNullOrEmpty(reviewParams.Sort))
            {
                switch (reviewParams.Sort)
                {
                    case "createDateAsc":
                        AddOrderBy(p => p.CreatedDate!);
                        break;

                    case "createDateDesc":
                        AddOrderByDescending(p => p.CreatedDate!);
                        break;

                    default:
                        AddOrderBy(p => p.CreatedDate!);
                        break;
                }
            }
            else
            {
                AddOrderByDescending(p => p.CreatedDate!);
            }
        }
    }
}
