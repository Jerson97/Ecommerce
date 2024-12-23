﻿using AutoMapper;
using Ecommerce.Application.Features.Addresses.Vms;
using Ecommerce.Application.Features.Categories.Vms;
using Ecommerce.Application.Features.Countries.Vms;
using Ecommerce.Application.Features.Images.Queries.Vms;
using Ecommerce.Application.Features.Orders.Vms;
using Ecommerce.Application.Features.Products.CreateProduct;
using Ecommerce.Application.Features.Products.Queries.Vms;
using Ecommerce.Application.Features.Products.UpdateProduct;
using Ecommerce.Application.Features.Reviews.Queries.Vms;
using Ecommerce.Application.Features.ShoppingCarts.Vms;
using Ecommerce.Domain;

namespace Ecommerce.Application.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Product, ProductVm>()
                .ForMember(p=> p.CategoryName, x => x.MapFrom(a => a.Category!.Name))
                .ForMember(p=> p.NumberReviews, x => x.MapFrom(a=> a.Reviews == null ? 0 : a.Reviews.Count));
            
            CreateMap<Image, ImageVm>();
            CreateMap<Review, ReviewVm>();
            CreateMap<Country, CountryVm>();
            CreateMap<Category, CategoryVm>();
            CreateMap<CreateProductCommand, Product>();
            CreateMap<CreateProductImageCommand, Image>();
            CreateMap<UpdateProductCommand, Product>();

            CreateMap<ShoppingCart, ShoppingCartVm>()
                .ForMember(p => p.ShoppingCartId, x => x.MapFrom(a => a.ShoppingCartMasterId));
            CreateMap<ShoppingCartItem, ShoppingCartItemVm>();

            CreateMap<ShoppingCartItemVm, ShoppingCartItem>();

            CreateMap<Address, AddressVm>();

            CreateMap<Order, OrderVm>();
            CreateMap<OrderItem, OrderItemVm>();
            CreateMap<OrderAddres, AddressVm>();
        }
    }
}
