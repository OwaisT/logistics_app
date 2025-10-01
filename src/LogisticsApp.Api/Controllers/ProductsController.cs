using Microsoft.AspNetCore.Mvc;
using LogisticsApp.Contracts.Product;
using LogisticsApp.Application.Products.Commands.CreateProduct;
using MediatR;
using MapsterMapper;
using LogisticsApp.Application.Products.Queries.GetProducts;
using Microsoft.AspNetCore.Authorization;
using LogisticsApp.Application.Products.Commands.AddReceivedForVariation;

namespace LogisticsApp.Api.Controllers;

[Route("products")]
public class ProductsController : ApiController
{
    private readonly ISender _mediator;
    private readonly IMapper _mapper;

    public ProductsController(ISender mediator, IMapper mapper)
    {
        _mediator = mediator;
        _mapper = mapper;
    }

    [Authorize(Roles = "BusinessManager")]
    [HttpPost]
    public async Task<IActionResult> CreateProduct(CreateProductRequest request, string hostId)
    {
        var command = _mapper.Map<CreateProductCommand>((request, hostId));
        var createProductResult = await _mediator.Send(command);
        return createProductResult.Match(
            product => Ok(_mapper.Map<ProductResponse>(product)),
            Problem);
    }

    [Authorize(Roles = "BusinessManager,FacilityManager,FacilityWorker")]
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

    [Authorize(Roles = "BusinessManager,FacilityManager")]
    [HttpPut("{productId}/VariationQuantity")]
    public async Task<IActionResult> AddReceivedForVariation(string productId, [FromBody] AddReceivedForVariationRequest request)
    {
        var command = _mapper.Map<AddReceivedForVariationCommand>((productId, request));
        var result = await _mediator.Send(command);
        return result.Match(
            product => Ok(_mapper.Map<ProductResponse>(product)),
            Problem);
    }
}