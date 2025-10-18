using LogisticsApp.Application.Aggregates.Products.Commands.Modifications.Variations.AddProductVariations;
using LogisticsApp.Application.Aggregates.Products.Commands.Modifications.Variations.RemoveProductVariations;
using LogisticsApp.Contracts.Aggregates.Product;
using LogisticsApp.Contracts.Aggregates.Product.Requests.Modifications.Variations;
using MapsterMapper;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LogisticsApp.Api.Controllers.AggregatesControllers.Products.Variations;

[Route("products/{productId}/variations")]
public class VariationsController(ISender mediator, IMapper mapper) : ApiController
{
    private readonly ISender _mediator = mediator;
    private readonly IMapper _mapper = mapper;

    [Authorize(Roles = "BusinessManager")]
    [HttpPost]
    public async Task<IActionResult> AddProductVariations(string productId, [FromBody] AddProductVariationsRequest request)
    {
        await Task.CompletedTask;
        var command = _mapper.Map<AddProductVariationsCommand>((productId, request));
        var result = await _mediator.Send(command);
        return result.Match(
            product => Ok(_mapper.Map<ProductResponse>(product)),
            Problem);
    }

    [Authorize(Roles = "BusinessManager")]
    [HttpDelete]
    public async Task<IActionResult> RemoveProductVariations(string productId, [FromBody] RemoveProductVariationsRequest request)
    {
        await Task.CompletedTask;
        var command = _mapper.Map<RemoveProductVariationsCommand>((productId, request));
        var result = await _mediator.Send(command);
        return result.Match(
            product => Ok(_mapper.Map<ProductResponse>(product)),
            Problem);
    }

}