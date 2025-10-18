using LogisticsApp.Application.Aggregates.Products.Commands.Modifications.Category.AddProductCategories;
using LogisticsApp.Application.Aggregates.Products.Commands.Modifications.Category.RemoveProductCategories;
using LogisticsApp.Contracts.Aggregates.Product;
using LogisticsApp.Contracts.Aggregates.Product.Requests.Modifications.Category;
using MapsterMapper;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LogisticsApp.Api.Controllers.AggregatesControllers.Products;

[Route("products/{productId}/categories")]
public class ProductCategoriesController(ISender mediator, IMapper mapper) : ApiController
{
    private readonly ISender _mediator = mediator;
    private readonly IMapper _mapper = mapper;

    [Authorize(Roles = "BusinessManager")]
    [HttpPost]
    public async Task<IActionResult> AddProductCategories(string productId, [FromBody] AddProductCategoriesRequest request)
    {
        await Task.CompletedTask;
        var command = _mapper.Map<AddProductCategoriesCommand>((productId, request));
        var result = await _mediator.Send(command);
        return result.Match(
            product => Ok(_mapper.Map<ProductResponse>(product)),
            Problem);
    }

    [Authorize(Roles = "BusinessManager")]
    [HttpDelete]
    public async Task<IActionResult> RemoveProductCategories(string productId, [FromBody] RemoveProductCategoriesRequest request)
    {
        await Task.CompletedTask;
        var command = _mapper.Map<RemoveProductCategoriesCommand>((productId, request));
        var result = await _mediator.Send(command);
        return result.Match(
            product => Ok(_mapper.Map<ProductResponse>(product)),
            Problem);
    }

}