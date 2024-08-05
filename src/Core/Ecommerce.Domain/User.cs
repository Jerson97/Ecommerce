using Ecommerce.Domain.Common;
using Microsoft.AspNetCore.Identity;

public class User : IdentityUser{

    public string? Name { get; set; }
    public string? LastName { get; set; }
    public string? Phone { get; set; }
    public string? AvatarUrl { get; set; }
    public bool isActive { get; set; } = true;

}