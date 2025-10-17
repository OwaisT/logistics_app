using LogisticsApp.Application.Aggregates.Products.Commands.Modifications.AddProductColor;
using LogisticsApp.Contracts.Aggregates.Product;
using LogisticsApp.Contracts.Aggregates.Product.Requests.Modifications.Color;
using MapsterMapper;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LogisticsApp.Api.Controllers.AggregatesControllers.Products;

[Route("products/{productId}/colors")]
public class ProductsColorController(ISender mediator, IMapper mapper) : ApiController
{
    private readonly ISender _mediator = mediator;
    private readonly IMapper _mapper = mapper;

    [Authorize(Roles = "BusinessManager")]
    [HttpPost]
    public async Task<IActionResult> AddProductColor(string productId, AddProductColorsRequest request)
    {
        await Task.CompletedTask;
        var command = _mapper.Map<AddProductColorsCommand>((productId, request));
        var result = await _mediator.Send(command);
        return result.Match(
            product => Ok(_mapper.Map<ProductResponse>(product)),
            Problem);
    }

}