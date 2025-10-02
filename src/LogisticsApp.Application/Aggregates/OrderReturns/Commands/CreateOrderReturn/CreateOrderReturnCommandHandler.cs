using ErrorOr;
using LogisticsApp.Application.Aggregates.OrderReturns.Commands.CreateOrderReturn;
using LogisticsApp.Application.Common.Interfaces.Persistence;
using LogisticsApp.Domain.BoundedContexts.Catalog.Aggregates.OrderAggregate.ValueObjects;
using LogisticsApp.Domain.BoundedContexts.Catalog.Aggregates.OrderReturnAggregate;
using LogisticsApp.Domain.BoundedContexts.Catalog.Aggregates.OrderReturnAggregate.Services;
using LogisticsApp.Domain.Common.Errors;
using MediatR;

namespace LogisticsApp.Application.Aggregates.OrderReturns.CreateOrderReturn;

public class CreateOrderReturnCommandHandler(
    IOrderRepository _orderRepository,
    IOrderReturnRepository _orderReturnRepository,
    OrderReturnCreationService _orderReturnCreationService)
     : IRequestHandler<CreateOrderReturnCommand, ErrorOr<OrderReturn>>
{
    public async Task<ErrorOr<OrderReturn>> Handle(CreateOrderReturnCommand command, CancellationToken cancellationToken)
    {
        await Task.CompletedTask;
        var order = _orderRepository.GetById(OrderId.Create(command.OrderId));
        if (order == null)
        {
            return Errors.Common.EntityNotFound(nameof(order), command.OrderId.ToString());
        }
        var orderItems = order.Items.Where(oi => command.OrderItemIds.Contains(oi.Id.Value)).ToList();
        if (orderItems.Count != command.OrderItemIds.Count)
        {
            return Errors.OrderReturn.InvalidReturnItems();
        }
        var orderReturnResult = _orderReturnCreationService.CreateOrderReturn(order.Id, orderItems);
        if (orderReturnResult.IsError)
        {
            return orderReturnResult.Errors;
        }
        _orderReturnRepository.Add(orderReturnResult.Value);

        return await Task.FromResult<ErrorOr<OrderReturn>>(orderReturnResult.Value);
    }
}