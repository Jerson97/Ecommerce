using Ecommerce.Domain.Common;

namespace Ecommerce.Domain;

public class OrderAddres : BaseDomainModel{

    public string? StreetAddress { get; set; }
    public string? City { get; set; }
    public string? Department { get; set; }
    public string? PostalCode { get; set; }
    public string? Username { get; set; }
    public string? Country { get; set; }
}