using Microsoft.AspNetCore.Mvc;
using LogisticsApp.Contracts.Product;
using LogisticsApp.Application.Product.Commands.CreateProduct;
using MediatR;

namespace LogisticsApp.Api.Controllers;

[Route("hosts/{hostId}/products")]
public class ProductController : ApiController
{
    private readonly ISender _mediator;

    public ProductController(ISender mediator)
    {
        _mediator = mediator;
    }

    [HttpPost]
    public IActionResult CreateProduct(CreateProductRequest request, string hostId)
    {
        return Ok(request);
    }
}