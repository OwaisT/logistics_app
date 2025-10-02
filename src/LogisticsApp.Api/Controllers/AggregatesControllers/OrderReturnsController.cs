using LogisticsApp.Application.Aggregates.OrderReturns.Commands.CreateOrderReturn;
using LogisticsApp.Application.Aggregates.OrderReturns.Commands.UpdateOrderReturnStatus;
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

    [Authorize(Roles = "BusinessManager,FacilityManager")]
    [HttpPut("{orderReturnId}/status")]
    public async Task<IActionResult> UpdateOrderReturnStatus(Guid orderReturnId, UpdateOrderReturnStatusRequest request)
    {
        var command = _mapper.Map<UpdateOrderReturnStatusCommand>((orderReturnId, request));
        var updateOrderReturnStatusResult = await _mediator.Send(command);
        return updateOrderReturnStatusResult.Match(
            orderReturn => Ok(_mapper.Map<OrderReturnResponse>(orderReturn)),
            Problem);
    }
}