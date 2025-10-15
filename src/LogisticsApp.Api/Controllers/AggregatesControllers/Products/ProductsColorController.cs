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
    public async Task<IActionResult> CreateProduct()
    {
        return Ok();
    }

}