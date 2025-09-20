using LogisticsApp.Application.Cartons.Commands.CreateCarton;
using LogisticsApp.Contracts.Carton;
using MapsterMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace LogisticsApp.Api.Controllers;

[Route("/Carton")]
public class CartonsController : ApiController
{
    private readonly ISender _mediator;

    private readonly IMapper _mapper;

    public CartonsController(ISender mediator, IMapper mapper)
    {
        _mediator = mediator;
        _mapper = mapper;
    }

    [HttpPost]
    public async Task<IActionResult> CreateCarton(CreateCartonRequest request)
    {
        var command = _mapper.Map<CreateCartonCommand>(request);
        var createCartonResult = await _mediator.Send(command);
        return createCartonResult.Match(
            carton => Ok(_mapper.Map<CartonResponse>(carton)),
            Problem);
    }

}
