using Ecommerce.Application.Features.Addresses.Vms;
using Ecommerce.Application.Models.Order;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Application.Features.Orders.Vms
{
    public class OrderVm
    {
        public int Id { get; set; }
        public AddressVm? OrderAddress { get; set; }
        public List<OrderItemVm?>? OrderItems { get; set; }
        public decimal SubTotal { get; set; }
        public decimal Tax { get; set; }
        public decimal Total { get; set; }
        public decimal ShippingPrice { get; set; }

        public OrderStatus? Status { get; set; }
        public string? PaymentIntentId { get; set; }
        public string? ClientSecret { get; set; }
        public string? StripeApiKey { get; set; }
        public string? UserBuyer { get; set; }
        public string? NameBuyer { get; set; }

        public int Quantity
        {
            get { return OrderItems!.Sum(x => x.Quantity); }
            set { }
        }
        public string? StatusLabel
        {
            get
            {
                switch (Status)
                {
                    case OrderStatus.Completed:
                        {
                            return OrderStatusLabel.COMPLETED;
                        }

                    case OrderStatus.Pending:
                        {
                            return OrderStatusLabel.PENDING;
                        }

                    case OrderStatus.Enviado:
                        {
                            return OrderStatusLabel.ENVIADO;
                        }

                    case OrderStatus.Error:
                        {
                            return OrderStatusLabel.ERROR;
                        }
                    default: return OrderStatusLabel.ERROR;
                        
                }
            }
        }
    }
}
