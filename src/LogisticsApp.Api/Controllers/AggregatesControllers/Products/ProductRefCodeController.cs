using LogisticsApp.Application.Aggregates.Products.Commands.Modifications.ModifyProductRefCode;
using LogisticsApp.Contracts.Aggregates.Product;
using LogisticsApp.Contracts.Aggregates.Product.Requests.Modifications;
using MapsterMapper;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LogisticsApp.Api.Controllers.AggregatesControllers.Products;

[Route("products/{productId}/refcode")]
public class ProductRefCodeController(ISender mediator, IMapper mapper) : ApiController
{
    private readonly ISender _mediator = mediator;
    private readonly IMapper _mapper = mapper;

    [Authorize(Roles = "BusinessManager")]
    [HttpPut]
    public async Task<IActionResult> ModifyProductRefCode(string productId, [FromBody] ModifyProductRefCodeRequest request)
    {
        await Task.CompletedTask;
        var command = _mapper.Map<ModifyProductRefCodeCommand>((productId, request));
        var result = await _mediator.Send(command);
        return result.Match(
            product => Ok(_mapper.Map<ProductResponse>(product)),
            Problem);
    }

}