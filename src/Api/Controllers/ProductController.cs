using Ecommerce.Application.Features.Products.Queries.GetProductById;
using Ecommerce.Application.Features.Products.Queries.GetProductList;
using Ecommerce.Application.Features.Products.Queries.PaginationProducts;
using Ecommerce.Application.Features.Products.Queries.Vms;
using Ecommerce.Application.Features.Shared.Queries;
using Ecommerce.Domain;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace Ecommerce.Api.Controllers;

[ApiController]
[Route("api/v1/[controller]")]
public class ProductController : ControllerBase{

    private IMediator _mediator;
    public ProductController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [AllowAnonymous]
    [HttpGet("list", Name = "GetProductList")]
    [ProducesResponseType(typeof(IReadOnlyList<ProductVm>), (int)HttpStatusCode.OK)]
    public async Task<ActionResult<IReadOnlyList<ProductVm>>> GetProductList(){
        var query = new GetProductListQuery();
        var products = await _mediator.Send(query);
        return Ok(products);
    }

    [AllowAnonymous]
    [HttpGet("pagination", Name = "PaginationProduct")]
    [ProducesResponseType(typeof(PaginationVm<ProductVm>),(int)HttpStatusCode.OK)]
    public async Task<ActionResult<PaginationVm<ProductVm>>> PaginationProduct([FromQuery] PaginationProductsQuery paginationProductsQuery)
    {
        paginationProductsQuery.Status = ProductStatus.Activo;
        var paginationProduct = await _mediator.Send(paginationProductsQuery);

        return Ok(paginationProduct);
    }

    [AllowAnonymous]
    [HttpGet("{id}", Name = "GetProductById")]
    [ProducesResponseType(typeof(ProductVm), (int)HttpStatusCode.OK)]
    public async Task<ActionResult<ProductVm>> GetProductById(int id)
    {
        var query = new GetProductByIdQuery(id);
        return Ok(await _mediator.Send(query));
    }

}