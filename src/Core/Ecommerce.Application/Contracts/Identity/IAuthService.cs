namespace Ecommerce.Application.Contracts.Identity
{
    public interface IAuthService
    {
        string GetSessionUser();
        //string CreateToken(User user, IList<string>? roles);
        string CreateToken(User user, List<string> roles);
    }
}
