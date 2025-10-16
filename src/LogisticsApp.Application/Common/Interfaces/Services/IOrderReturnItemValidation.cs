using ErrorOr;
using LogisticsApp.Domain.BoundedContexts.Catalog.Aggregates.OrderAggregate.Entities;
using LogisticsApp.Domain.BoundedContexts.Catalog.Aggregates.OrderAggregate.ValueObjects;

namespace LogisticsApp.Application.Common.Interfaces.Services;

public interface IOrderReturnItemsValidation
{
    ErrorOr<List<OrderItem>> ValidateAndGetOrderItemsForReturn(OrderId orderId, List<Guid> orderItemIds);
}