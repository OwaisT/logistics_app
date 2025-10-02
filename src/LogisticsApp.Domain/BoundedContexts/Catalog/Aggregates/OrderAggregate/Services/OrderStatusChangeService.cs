using ErrorOr;
using LogisticsApp.Domain.BoundedContexts.Catalog.Aggregates.OrderAggregate.Policies;

namespace LogisticsApp.Domain.BoundedContexts.Catalog.Aggregates.OrderAggregate.Services;

public class OrderStatusChangeService(
    IEnumerable<IOrderStatusChangePolicy> _policies)
{
    // This class can be expanded in the future to include more complex business logic related to order status changes.

    public ErrorOr<Order> ChangeOrderStatus(Order order, string newStatus)
    {
        foreach (var policy in _policies)
        {
            var result = policy.CanChangeStatus(order, newStatus);
            if (result.IsError)
            {
                return result.Errors;
            }
        }

        return order.UpdateStatus(newStatus);
    }
}