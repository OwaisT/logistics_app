using Microsoft.AspNetCore.Mvc;
using LogisticsApp.Contracts.Product;
using LogisticsApp.Application.Products.Commands.CreateProduct;
using MediatR;
using MapsterMapper;
using LogisticsApp.Application.Products.Queries.GetProducts;

namespace LogisticsApp.Api.Controllers;

[Route("hosts/{hostId}/products")]
public class ProductsController : ApiController
{
    private readonly ISender _mediator;
    private readonly IMapper _mapper;

    public ProductsController(ISender mediator, IMapper mapper)
    {
        _mediator = mediator;
        _mapper = mapper;
    }

    [HttpPost]
    public async Task<IActionResult> CreateProduct(CreateProductRequest request, string hostId)
    {
        var command = _mapper.Map<CreateProductCommand>((request, hostId));
        var createProductResult = await _mediator.Send(command);
        return createProductResult.Match(
            product => Ok(_mapper.Map<ProductResponse>(product)),
            Problem);
    }

    [HttpGet]
    public async Task<IActionResult> GetProducts()
    {
        // Implementation for retrieving products would go here
        var query = new GetProductsQuery();
        var productsResult = await _mediator.Send(query);
        return productsResult.Match(
            products => Ok(_mapper.Map<List<ProductResponse>>(products)),
            Problem);
    }
}