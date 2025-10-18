using LogisticsApp.Application.Aggregates.Products.Commands.Modifications.Size.AddProductSizes;
using LogisticsApp.Application.Aggregates.Products.Commands.Modifications.Size.RemoveProductSizes;
using LogisticsApp.Contracts.Aggregates.Product;
using LogisticsApp.Contracts.Aggregates.Product.Requests.Modifications.Size;
using MapsterMapper;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LogisticsApp.Api.Controllers.AggregatesControllers.Products;

[Route("products/{productId}/sizes")]
public class ProductSizesController(ISender mediator, IMapper mapper) : ApiController
{
    private readonly ISender _mediator = mediator;
    private readonly IMapper _mapper = mapper;

    [Authorize(Roles = "BusinessManager")]
    [HttpPost]
    public async Task<IActionResult> AddProductSizes(string productId, [FromBody] AddProductSizesRequest request)
    {
        await Task.CompletedTask;
        var command = _mapper.Map<AddProductSizesCommand>((productId, request));
        var result = await _mediator.Send(command);
        return result.Match(
            product => Ok(_mapper.Map<ProductResponse>(product)),
            Problem);
    }

    [Authorize(Roles = "BusinessManager")]
    [HttpDelete]
    public async Task<IActionResult> RemoveProductSizes(string productId, [FromBody] RemoveProductSizesRequest request)
    {
        await Task.CompletedTask;
        var command = _mapper.Map<RemoveProductSizesCommand>((productId, request));
        var result = await _mediator.Send(command);
        return result.Match(
            product => Ok(_mapper.Map<ProductResponse>(product)),
            Problem);
    }

}