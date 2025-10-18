using LogisticsApp.Application.Aggregates.Products.Commands.Modifications.Variations.ModifyVariationsPrice;
using LogisticsApp.Contracts.Aggregates.Product;
using LogisticsApp.Contracts.Aggregates.Product.Requests.Modifications.Variations;
using MapsterMapper;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LogisticsApp.Api.Controllers.AggregatesControllers.Products.Variations;

// Controller for managing product variations' prices
// The prices for variations are changed for an entire color and not individually per variation
[Route("products/{productId}/variations/price")]
public class VariationsPriceController(ISender mediator, IMapper mapper) : ApiController
{
    private readonly ISender _mediator = mediator;
    private readonly IMapper _mapper = mapper;

    [Authorize(Roles = "BusinessManager")]
    [HttpPut]
    public async Task<IActionResult> ModifyVariationsPrice(string productId, [FromBody] ModifyVariationsPriceRequest request)
    {
        await Task.CompletedTask;
        var command = _mapper.Map<ModifyVariationsPriceCommand>((productId, request));
        var result = await _mediator.Send(command);
        return result.Match(
            product => Ok(_mapper.Map<ProductResponse>(product)),
            Problem);
    }

}