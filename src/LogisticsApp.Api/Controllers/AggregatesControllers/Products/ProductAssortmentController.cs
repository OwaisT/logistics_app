using LogisticsApp.Application.Aggregates.Products.Commands.Modifications.ModifyProductAssortments;
using LogisticsApp.Contracts.Aggregates.Product;
using LogisticsApp.Contracts.Aggregates.Product.Requests.Modifications;
using MapsterMapper;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LogisticsApp.Api.Controllers.AggregatesControllers.Products;

[Route("products/{productId}/assortments")]
public class ProductAssortmentController(ISender mediator, IMapper mapper) : ApiController
{
    private readonly ISender _mediator = mediator;
    private readonly IMapper _mapper = mapper;

    [Authorize(Roles = "BusinessManager")]
    [HttpPut]
    public async Task<IActionResult> ModifyProductAssortments(string productId, [FromBody] ModifyProductAssortmentsRequest request)
    {
        await Task.CompletedTask;
        var command = _mapper.Map<ModifyProductAssortmentsCommand>((productId, request));
        var result = await _mediator.Send(command);
        return result.Match(
            product => Ok(_mapper.Map<ProductResponse>(product)),
            Problem);
    }

}