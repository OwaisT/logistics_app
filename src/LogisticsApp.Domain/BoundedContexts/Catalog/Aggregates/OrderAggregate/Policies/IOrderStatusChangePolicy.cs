using ErrorOr;

namespace LogisticsApp.Domain.BoundedContexts.Catalog.Aggregates.OrderAggregate.Policies;

public interface IOrderStatusChangePolicy
{
    ErrorOr<bool> CanChangeStatus(Order order, string newStatus);
}