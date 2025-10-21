using Microsoft.AspNetCore.Mvc;
using MediatR;
using MapsterMapper;
using Microsoft.AspNetCore.Authorization;
using LogisticsApp.Contracts.Aggregates.Product;
using LogisticsApp.Application.Aggregates.Products.Commands.CreateProduct;
using LogisticsApp.Application.Aggregates.Products.Queries.GetProducts;
using LogisticsApp.Contracts.Aggregates.Product.Requests;
using LogisticsApp.Contracts.Aggregates.Product.Requests.Modifications.Variations;
using LogisticsApp.Application.Aggregates.Products.Commands.Modifications.Variations.AddReceivedForVariation;

namespace LogisticsApp.Api.Controllers.AggregatesControllers.Products;

[Route("products")]
public class ProductsController(ISender mediator, IMapper mapper) : ApiController
{
    private readonly ISender _mediator = mediator;
    private readonly IMapper _mapper = mapper;

    [Authorize(Roles = "BusinessManager")]
    [HttpPost]
    public async Task<IActionResult> CreateProduct(CreateProductRequest request)
    {
        var command = _mapper.Map<CreateProductCommand>(request);
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

    // TODO: Move to a separate controller
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