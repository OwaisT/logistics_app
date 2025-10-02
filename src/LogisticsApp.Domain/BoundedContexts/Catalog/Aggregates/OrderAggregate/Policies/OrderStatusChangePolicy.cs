using ErrorOr;

namespace LogisticsApp.Domain.BoundedContexts.Catalog.Aggregates.OrderAggregate.Policies;

public class OrderStatusChangePolicy : IOrderStatusChangePolicy
{
    private static readonly Dictionary<string, List<string>> AllowedTransitions = new()
    {
        { "Pending", [ "Processing", "Cancelled" ] },
        { "Processing", [ "Shipped", "Cancelled" ] },
        { "Shipped", [ "Delivered", "Returned" ] },
        { "Delivered", [ "Returned" ] },
        { "Cancelled", [] },
        { "Returned", [] }
    };

    public ErrorOr<bool> CanChangeStatus(Order order, string newStatus)
    {
        if (AllowedTransitions.TryGetValue(order.Status, out var allowedStatuses))
        {
            if (allowedStatuses.Contains(newStatus))
            {
                return true;
            }
        }
        // TODO: Create a specific error type for this
        Error error = Error.Validation(
            code: "Order.StatusChange.NotAllowed",
            description: $"Cannot change order status from {order.Status} to {newStatus}."
        );
        return error;

    }
}