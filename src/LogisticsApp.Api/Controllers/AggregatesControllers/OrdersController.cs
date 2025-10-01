using LogisticsApp.Application.Aggregates.Orders.Commands.CreateOrder;
using LogisticsApp.Application.Aggregates.Orders.Commands.UpdateOrderStatus;
using LogisticsApp.Contracts.Aggregates.Order;
using MapsterMapper;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LogisticsApp.Api.Controllers.AggregatesControllers;

[Route("/Orders")]
public class OrdersController(ISender mediator, IMapper mapper) : ApiController
{
    private readonly ISender _mediator = mediator;

    private readonly IMapper _mapper = mapper;

    [Authorize(Roles = "BusinessManager")]
    [HttpPost]
    public async Task<IActionResult> CreateOrder(CreateOrderRequest request)
    {
        var command = _mapper.Map<CreateOrderCommand>(request);
        var createOrderResult = await _mediator.Send(command);
        return createOrderResult.Match(
            order => Ok(_mapper.Map<OrderResponse>(order)),
            Problem);
    }

    [Authorize(Roles = "BusinessManager,FacilityManager,FacilityWorker")]
    [HttpPut("{orderId}/Status")]
    public async Task<IActionResult> UpdateOrderStatus(Guid orderId, UpdateOrderStatusRequest request)
    {
        var command = _mapper.Map<UpdateOrderStatusCommand>((orderId, request));

        var updateOrderStatusResult = await _mediator.Send(command);
        return updateOrderStatusResult.Match(
            order => Ok(_mapper.Map<OrderResponse>(order)),
            Problem);
    }
}