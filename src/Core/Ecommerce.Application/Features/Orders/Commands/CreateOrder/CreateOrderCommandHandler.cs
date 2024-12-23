﻿using AutoMapper;
using Ecommerce.Application.Contracts.Identity;
using Ecommerce.Application.Features.Orders.Vms;
using Ecommerce.Application.Models.Payment;
using Ecommerce.Application.Persistence;
using Ecommerce.Domain;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Stripe;
using System.Linq.Expressions;

namespace Ecommerce.Application.Features.Orders.Commands.CreateOrder;

public class CreateOrderCommandHandler : IRequestHandler<CreateOrderCommand, OrderVm>
{

    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    private readonly IAuthService _authService;
    private readonly UserManager<User> _userManager;
    private readonly StripeSettings _stripeSettings;

    public CreateOrderCommandHandler(IUnitOfWork unitOfWork,
        IMapper mapper,
        IAuthService authService,
        UserManager<User> userManager,
        IOptions<StripeSettings> stripeSettings
    )
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        _authService = authService;
        _userManager = userManager;
        _stripeSettings = stripeSettings.Value;
    }

    public async Task<OrderVm> Handle(CreateOrderCommand request, CancellationToken cancellationToken)
    {
        var orderPending = await _unitOfWork.Repository<Order>().GetEntityAsync(
            x => x.BuyerName == _authService.GetSessionUser() && x.Status == OrderStatus.Pending,
            null,
            true
        );

        if (orderPending is not null)
        {
            await _unitOfWork.Repository<Order>().DeleteAsync(orderPending);
        }


        var includes = new List<Expression<Func<ShoppingCart, object>>>();
        includes.Add(p => p.ShoppingCartItems!.OrderBy(x => x.Product));


        var shoppingCart = await _unitOfWork.Repository<ShoppingCart>().GetEntityAsync(
            x => x.ShoppingCartMasterId == request.ShoppingCartId,
            includes,
            false
        );

        var user = await _userManager.FindByNameAsync(_authService.GetSessionUser());
        if (user is null)
        {
            throw new Exception("El usuario no esta autenticado");
        }

        var direccion = await _unitOfWork.Repository<Ecommerce.Domain.Address>().GetEntityAsync(
            x => x.Username == user.UserName,
            null,
            false
        );

        OrderAddres orderAddress = new()
        {
            StreetAddress = direccion.StreetAddress,
            City = direccion.City,
            PostalCode = direccion.PostalCode,
            Country = direccion.Country,
            Department = direccion.Department,
            Username = direccion.Username
        };

        await _unitOfWork.Repository<OrderAddres>().AddAsync(orderAddress);

        var subtotal = Math.Round(shoppingCart.ShoppingCartItems!.Sum(x => x.Price * x.Quantity), 2);
        var impuesto = Math.Round(subtotal * Convert.ToDecimal(0.18), 2);
        var precioEnvio = subtotal < 100 ? 10 : 25;
        var total = subtotal + impuesto + precioEnvio;

        var nombreComprador = $"{user.Name}  {user.LastName}";
        var order = new Order(nombreComprador, user.UserName!, orderAddress, subtotal, total, impuesto, precioEnvio);

        await _unitOfWork.Repository<Order>().AddAsync(order);

        var items = new List<OrderItem>();

        foreach (var shoppingElement in shoppingCart.ShoppingCartItems!)
        {
            var orderItem = new OrderItem
            {
                ProductName = shoppingElement.Product,
                ProductId = shoppingElement.ProductId,
                ImageUrl = shoppingElement.Image,
                Price = shoppingElement.Price,
                Cantidad = shoppingElement.Quantity,
                OrderId = order.Id
            };

            items.Add(orderItem);
        }

        _unitOfWork.Repository<OrderItem>().AddRange(items);

        var resultado = await _unitOfWork.Complete();

        if (resultado <= 0)
        {
            throw new Exception("Error creando la orden de compra");
        }

        StripeConfiguration.ApiKey = _stripeSettings.SecretKey;
        var service = new PaymentIntentService();
        PaymentIntent intent;

        if (string.IsNullOrEmpty(order.PaymentIntentId))
        {
            var options = new PaymentIntentCreateOptions
            {
                Amount = (long)order.Total,
                Currency = "usd",
                PaymentMethodTypes = new List<string> { "card" }
            };

            intent = await service.CreateAsync(options);
            order.PaymentIntentId = intent.Id;
            order.ClientSecret = intent.ClientSecret;
            order.StripeApiKey = _stripeSettings.Publishblekey;
        }
        else
        {
            var options = new PaymentIntentUpdateOptions
            {
                Amount = (long)order.Total
            };
            await service.UpdateAsync(order.PaymentIntentId, options);
        }


        _unitOfWork.Repository<Order>().UpdateEntity(order);
        var resultadoOrder = await _unitOfWork.Complete();

        if (resultadoOrder <= 0)
        {
            throw new Exception("Error creando el payment intent en stripe");
        }

        return _mapper.Map<OrderVm>(order);
    }
}