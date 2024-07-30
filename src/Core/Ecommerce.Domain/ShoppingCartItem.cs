using Ecommerce.Domain.Common;

namespace Ecommerce.Domain;

public class ShoppingCartItem : BaseDomainModel{

    public string? Product { get; set; }
    public decimal Price { get; set; }
    public int Quantity  { get; set; }
    public decimal Image { get; set; }
    public decimal Category { get; set; }
    public Guid? ShoppingCartMasterId { get; set; }
    public int ShoppingCartId { get; set; }
    public int ProductId { get; set; }
    public int Stock { get; set; }
    public ShoppingCart? ShoppingCart { get; set; }
}