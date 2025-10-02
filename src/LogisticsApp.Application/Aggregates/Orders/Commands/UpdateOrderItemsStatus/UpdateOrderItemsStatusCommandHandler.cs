using ErrorOr;
using LogisticsApp.Application.Common.Interfaces.Persistence;
using LogisticsApp.Domain.BoundedContexts.Catalog.Aggregates.OrderAggregate;
using LogisticsApp.Domain.BoundedContexts.Catalog.Aggregates.OrderAggregate.Services;
using LogisticsApp.Domain.BoundedContexts.Catalog.Aggregates.OrderAggregate.ValueObjects;
using LogisticsApp.Domain.Common.Errors;
using MediatR;

namespace LogisticsApp.Application.Aggregates.Orders.Commands.UpdateOrderItemsStatus;

public class UpdateOrderItemsStatusCommandHandler(
    IOrderRepository _orderRepository,
    OrderItemsStatusChangeService _orderStatusChangeService)
     : IRequestHandler<UpdateOrderItemsStatusCommand, ErrorOr<Order>>
{
    public async Task<ErrorOr<Order>> Handle(UpdateOrderItemsStatusCommand command, CancellationToken cancellationToken)
    {
        await Task.CompletedTask;
        var orderId = OrderId.Create(command.OrderId);
        var order = _orderRepository.GetById(orderId);
        if (order is null)
        {
            return Errors.Common.EntityNotFound(nameof(Order), orderId.Value.ToString());
        }
        var updateStatusResult = _orderStatusChangeService.ChangeOrderItemsStatus(order, command.OrderItemsIds, command.Status);
        if (updateStatusResult.IsError)
        {
            return updateStatusResult.Errors;
        }
        _orderRepository.Update(order);
        return await Task.FromResult<ErrorOr<Order>>(order);
    }
}