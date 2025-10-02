using ErrorOr;
using LogisticsApp.Application.Common.Interfaces.Persistence;
using LogisticsApp.Domain.BoundedContexts.Catalog.Aggregates.OrderAggregate;
using LogisticsApp.Domain.BoundedContexts.Catalog.Aggregates.OrderAggregate.Entities;
using LogisticsApp.Domain.BoundedContexts.Catalog.Aggregates.OrderAggregate.ValueObjects;
using LogisticsApp.Domain.BoundedContexts.Catalog.Aggregates.OrderReturnAggregate.Services;
using LogisticsApp.Domain.Common.Errors;

namespace LogisticsApp.Application.Aggregates.OrderReturns.Services;

public class OrderReturnItemsValidation(
    IOrderRepository _orderRepository)
     : IOrderReturnItemsValidation
{
    public ErrorOr<bool> IsValidOrderItemsForReturn(OrderId orderId, List<OrderItem> orderItems)
    {
        var order = _orderRepository.GetById(orderId);
        if (order == null)
        {
            return Errors.Common.EntityNotFound(nameof(Order), orderId.Value.ToString());
        }
        foreach (var item in orderItems)
        {
            if (order.Items.Any(oi => oi.Id == item.Id && oi.Status == "Returned"))
            {
                return Errors.OrderReturn.InvalidReturnItem(item.Id.Value.ToString());
            }
        }

        return orderItems != null && orderItems.Count > 0;
    }
}