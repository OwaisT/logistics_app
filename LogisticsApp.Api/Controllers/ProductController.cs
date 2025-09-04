using Microsoft.AspNetCore.Mvc;
using LogisticsApp.Contracts.Product;
using LogisticsApp.Application.Product.Commands.CreateProduct;
using MediatR;

namespace LogisticsApp.Api.Controllers;

[Route("[controller]")]
public class ProductController : ApiController
{
    private readonly ISender _mediator;

    public ProductController(ISender mediator)
    {
        _mediator = mediator;
    }


    [HttpPost("create")]
    public async Task<IActionResult> CreateProduct(CreateProductRequest request)
    {
        // var product = _productService.CreateProduct(request.ProductRef, request.Season);
        var command = new CreateProductCommand(request.ProductRef, request.Season);
        var product = await _mediator.Send(command);
        var response = new ProductResponse(
            product.ProductId,
            product.ProductRef,
            product.Season
        );
        return Ok(response);
    }
}