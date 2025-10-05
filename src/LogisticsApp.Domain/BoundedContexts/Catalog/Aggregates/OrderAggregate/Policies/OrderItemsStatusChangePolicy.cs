using ErrorOr;
using LogisticsApp.Domain.BoundedContexts.Catalog.Aggregates.OrderAggregate.Entities;

namespace LogisticsApp.Domain.BoundedContexts.Catalog.Aggregates.OrderAggregate.Policies;

public class OrderItemsStatusChangePolicy : IOrderItemsStatusChangePolicy
{
    private static readonly Dictionary<string, List<string>> AllowedTransitions = new()
    {
        { "Pending",    new List<string> { "Processing", "Cancelled" } },
        { "Processing", new List<string> { "Shipped", "Cancelled" } },
        { "Shipped",    new List<string> { "Delivered", "Returned" } },
        { "Delivered",  new List<string> { "Returned" } },
        { "Cancelled",  new List<string>() },
        { "Returned",   new List<string>() }
    };

    public ErrorOr<bool> CanChangeStatus(Order order, List<OrderItem> orderItems, string newStatus)
    {
        foreach (var item in orderItems)
        {
            if (!order.Items.Contains(item))
            {
                // TODO: Create a specific error type for this
                return Error.Failure($"Order does not contain item with ID: {item.Id.Value}");
            }

            // 1. Basic transition check
            if (!IsValidStatusTransition(item.Status, newStatus))
            {
                // TODO: Create a specific error type for this
                return Error.Failure(
                    $"Invalid status transition for item ID {item.Id.Value} from {item.Status} to {newStatus}");
            }

            // 2. Guardrails based on order-level status
            if (!IsAllowedByOrderStatus(order.Status, newStatus))
            {
                // TODO: Create a specific error type for this
                return Error.Failure(
                    $"Order status '{order.Status}' does not allow items to transition to '{newStatus}' (item ID {item.Id})");
            }
        }

        return true;
    }

    private static bool IsValidStatusTransition(string currentStatus, string newStatus)
    {
        return AllowedTransitions.TryGetValue(currentStatus, out var allowedStatuses) 
               && allowedStatuses.Contains(newStatus);
    }

    // TODO: Need to enforce more guardrails based on business rules
    private static bool IsAllowedByOrderStatus(string orderStatus, string newStatus)
    {
        // Example guardrails
        return orderStatus switch
        {
            // Items cannot be returned if order is Pending or Processing
            "Pending" or "Processing" when newStatus == "Returned" => false,

            // Items cannot be changed at all if order is Cancelled
            "Cancelled" => false,

            // Items cannot be changed if order is Completed
            "Completed" => false,

            _ => true
        };
    }
}