using LogisticsApp.Application.Cartons.Commands.AddCartonItem;
using LogisticsApp.Application.Cartons.Commands.CreateCarton;
using LogisticsApp.Contracts.Carton;
using MapsterMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace LogisticsApp.Api.Controllers;

[Route("/Cartons")]
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

    [HttpPost("{cartonId}/AddItem")]
    public async Task<IActionResult> AddCartonItem(AddCartonItemRequest request, string cartonId)
    {
        var command = _mapper.Map<AddCartonItemCommand>((request, cartonId));
        var addCartonItemResult = await _mediator.Send(command);
        return addCartonItemResult.Match(
            carton => Ok(_mapper.Map<CartonResponse>(carton)),
            Problem);
    }

}
