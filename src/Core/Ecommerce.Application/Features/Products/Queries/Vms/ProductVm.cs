﻿using Ecommerce.Application.Features.Images.Queries.Vms;
using Ecommerce.Application.Features.Reviews.Queries.Vms;
using Ecommerce.Application.Models.Product;
using Ecommerce.Domain;

namespace Ecommerce.Application.Features.Products.Queries.Vms
{
    public class ProductVm
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public decimal Price { get; set; }
        public int Rating { get; set; }
        public string? Seller { get; set; }
        public int Stock { get; set; }
        //public string? Description { get; set; }
        public virtual ICollection<ReviewVm>? Reviews { get; set; }
        public virtual ICollection<ImageVm>? Images { get; set; }
        public int CategoryId { get; set; }
        public string? CategoryName { get; set; }
        public int NumberReviews { get; set; }
        public ProductStatus Status { get; set; }
        public string StatusLabel
        {
            get
            {
                switch (Status)
                {
                    case ProductStatus.Activo:
                        {
                            return ProductStatusLabel.ACTIVO;
                        }
                    case ProductStatus.Inactivo:
                        {
                            return ProductStatusLabel.INACTIVO;
                        }
                    default: return ProductStatusLabel.INACTIVO;
                }
            }
            set { }
        }
    }
}
