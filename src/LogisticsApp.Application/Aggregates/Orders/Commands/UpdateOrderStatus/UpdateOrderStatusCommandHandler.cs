using ErrorOr;
using LogisticsApp.Application.Common.Interfaces.Persistence;
using LogisticsApp.Domain.BoundedContexts.Catalog.Aggregates.Order;
using LogisticsApp.Domain.BoundedContexts.Catalog.Aggregates.Order.ValueObjects;
using LogisticsApp.Domain.Common.Errors;
using MediatR;

namespace LogisticsApp.Application.Aggregates.Orders.Commands.UpdateOrderStatus;

public class UpdateOrderStatusCommandHandler(
    IOrderRepository _orderRepository)
     : IRequestHandler<UpdateOrderStatusCommand, ErrorOr<Order>>
{
    public async Task<ErrorOr<Order>> Handle(UpdateOrderStatusCommand command, CancellationToken cancellationToken)
    {
        await Task.CompletedTask;
        var orderId = OrderId.Create(command.OrderId);
        var order = _orderRepository.GetById(orderId);
        if (order is null)
        {
            return Errors.Common.EntityNotFound(nameof(Order), orderId.Value.ToString());
        }
        var updateStatusResult = order.UpdateStatus(command.Status);
        if (updateStatusResult.IsError)
        {
            return updateStatusResult.Errors;
        }
        _orderRepository.Update(order);
        return await Task.FromResult<ErrorOr<Order>>(order);
    }
}