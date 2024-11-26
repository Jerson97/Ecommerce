namespace Ecommerce.Application.Features.ShoppingCarts.Vms
{
    public class ShoppingCartItemVm
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public string? Product { get; set; }
        public decimal Price { get; set; }
        public int Amount { get; set; }
        public string? Category { get; set; }
        public int Stock { get; set; }

        public decimal TotalLine
        {
            get
            {
                return Math.Round(Amount * Price, 2);
            }
        }
    }
}
