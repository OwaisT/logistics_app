using ErrorOr;
using LogisticsApp.Domain.BoundedContexts.Catalog.Aggregates.OrderAggregate.Entities;

namespace LogisticsApp.Domain.BoundedContexts.Catalog.Aggregates.OrderAggregate.Policies;

public interface IOrderItemsStatusChangePolicy
{
    ErrorOr<bool> CanChangeStatus(Order order, List<OrderItem> orderItems, string newStatus);
}