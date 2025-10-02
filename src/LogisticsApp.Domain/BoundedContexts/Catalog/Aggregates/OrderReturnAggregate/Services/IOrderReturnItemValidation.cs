using ErrorOr;
using LogisticsApp.Domain.BoundedContexts.Catalog.Aggregates.OrderAggregate.Entities;
using LogisticsApp.Domain.BoundedContexts.Catalog.Aggregates.OrderAggregate.ValueObjects;

namespace LogisticsApp.Domain.BoundedContexts.Catalog.Aggregates.OrderReturnAggregate.Services;

public interface IOrderReturnItemsValidation
{
    ErrorOr<bool> IsValidOrderItemsForReturn(OrderId orderId, List<OrderItem> orderItems);
}