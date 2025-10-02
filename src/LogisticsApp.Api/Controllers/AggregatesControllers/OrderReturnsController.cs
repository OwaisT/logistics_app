using LogisticsApp.Application.Aggregates.OrderReturns.Commands.CreateOrderReturn;
using LogisticsApp.Contracts.Aggregates.OrderReturn;
using MapsterMapper;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LogisticsApp.Api.Controllers.AggregatesControllers;

[Route("/OrderReturns")]
public class OrderReturnsController(ISender mediator, IMapper mapper) : ApiController
{
    private readonly ISender _mediator = mediator;

    private readonly IMapper _mapper = mapper;

    [Authorize(Roles = "BusinessManager")]
    [HttpPost]
    public async Task<IActionResult> CreateOrderReturn(CreateOrderReturnRequest request)
    {
        var command = _mapper.Map<CreateOrderReturnCommand>(request);
        var createOrderReturnResult = await _mediator.Send(command);
        return createOrderReturnResult.Match(
            order => Ok(_mapper.Map<OrderReturnResponse>(order)),
            Problem);
    }
}