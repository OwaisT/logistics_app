using Microsoft.AspNetCore.Mvc;
using LogisticsApp.Contracts.Product;
using LogisticsApp.Application.Products.Commands.CreateProduct;
using MediatR;
using MapsterMapper;

namespace LogisticsApp.Api.Controllers;

[Route("hosts/{hostId}/products")]
public class ProductsController : ApiController
{
    private readonly ISender _mediator;
    private readonly IMapper _mapper;

    public ProductsController(ISender mediator, IMapper mapper)
    {
        _mediator = mediator;
        _mapper = mapper;
    }

    [HttpPost]
    public async Task<IActionResult> CreateProduct(CreateProductRequest request, string hostId)
    {
        var command = _mapper.Map<CreateProductCommand>((request, hostId));
        var createProductResult = await _mediator.Send(command);
        return createProductResult.Match(
            product => Ok(_mapper.Map<ProductResponse>(product)),
            Problem);
    }
}