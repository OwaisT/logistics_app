using ErrorOr;
using LogisticsApp.Application.Common.Interfaces.Persistence;
using LogisticsApp.Application.Common.Interfaces.Services;
using LogisticsApp.Domain.BoundedContexts.Catalog.Aggregates.OrderAggregate;
using LogisticsApp.Domain.BoundedContexts.Catalog.Aggregates.OrderAggregate.Entities;
using LogisticsApp.Domain.BoundedContexts.Catalog.Aggregates.OrderAggregate.ValueObjects;
using LogisticsApp.Domain.Common.Errors;

namespace LogisticsApp.Application.Aggregates.OrderReturns.Services;

public class OrderReturnItemsValidation(
    IOrderRepository _orderRepository)
     : IOrderReturnItemsValidation
{
    public ErrorOr<List<OrderItem>> ValidateAndGetOrderItemsForReturn(OrderId orderId, List<Guid> orderItemIds)
    {
        var order = _orderRepository.GetById(orderId);
        if (order == null)
        {
            return Errors.Common.EntityNotFound(nameof(Order), orderId.Value.ToString());
        }
        var orderItems = order.Items.Where(oi => orderItemIds.Contains(oi.Id.Value)).ToList();
        if (orderItems.Count != orderItemIds.Count)
        {
            return Errors.OrderReturn.InvalidReturnItems();
        }
        foreach (var item in orderItems)
        {
            if (order.Items.Any(oi => oi.Id == item.Id && oi.Status == "Returned"))
            {
                return Errors.OrderReturn.InvalidReturnItem(item.Id.Value.ToString());
            }
        }

        return orderItems;
    }
}