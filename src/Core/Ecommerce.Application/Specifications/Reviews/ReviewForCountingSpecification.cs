using Ecommerce.Domain;

namespace Ecommerce.Application.Specifications.Reviews
{
    public class ReviewForCountingSpecification : BaseSpecification<Review>
    {
        public ReviewForCountingSpecification(ReviewSpecificationParam reviewParams)
            : base(
                  x =>(!reviewParams.ProductoId.HasValue || x.ProductId == reviewParams.ProductoId))
        {
            
        }
    }
}
