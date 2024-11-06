namespace Ecommerce.Application.Specifications.Users
{
    public class UserSpecification : BaseSpecification<User>
    {
        public UserSpecification(UserSpecificationParams userParams) : base(
            x =>
            (string.IsNullOrEmpty(userParams.Search) || x.Name!.Contains(userParams.Search)
            || x.LastName!.Contains(userParams.Search) || x.Email!.Contains(userParams.Search)))
        {
            ApplyPagin(userParams.PageSize * (userParams.PageIndex - 1), userParams.PageSize);

            if (!string.IsNullOrEmpty(userParams.Sort))
            {
                switch (userParams.Sort)
                {
                    case "nameAsc":
                        AddOrderBy(p => p.Name!);
                        break;

                    case "nameDesc": 
                        AddOrderByDescending(p => p.LastName!);
                        break;
                    case "lastNameAsc":
                        AddOrderBy(p => p.Name!);
                        break;

                    case "lastNameDesc":
                        AddOrderByDescending(p => p.LastName!);
                        break;
                    default:
                        AddOrderBy(p => p.Name!);
                        break;
                }
            }
            else
            {
                AddOrderByDescending(p => p.Name!);
            }

        }
    }
}
