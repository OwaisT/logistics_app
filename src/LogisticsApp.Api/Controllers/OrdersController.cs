

using LogisticsApp.Application.Orders.CreateOrder;
using LogisticsApp.Contracts.Order;
using MapsterMapper;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LogisticsApp.Api.Controllers;

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
}