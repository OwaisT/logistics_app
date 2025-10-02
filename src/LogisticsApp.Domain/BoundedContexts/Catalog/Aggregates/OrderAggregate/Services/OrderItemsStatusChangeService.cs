using ErrorOr;
using LogisticsApp.Domain.BoundedContexts.Catalog.Aggregates.OrderAggregate.Policies;

namespace LogisticsApp.Domain.BoundedContexts.Catalog.Aggregates.OrderAggregate.Services;
 
public class OrderItemsStatusChangeService(
    IEnumerable<IOrderItemsStatusChangePolicy> _policies)
{
    // This class can be expanded in the future to include more complex business logic related to order status changes.

    public ErrorOr<Order> ChangeOrderItemsStatus(Order order, List<Guid> orderItemsIds, string newStatus)
    {
        var orderItems = order.Items.Where(oi => orderItemsIds.Contains(oi.Id.Value)).ToList();
        if (orderItems.Count != orderItemsIds.Count)
        {
            return Error.Validation(
                code: "OrderItems.NotFound",
                description: "One or more order items were not found in the order.");
        }
        foreach (var policy in _policies)
        {
            var result = policy.CanChangeStatus(order, orderItems, newStatus);
            if (result.IsError)
            {
                return result.Errors;
            }
        }

        return order.UpdateItemsStatus(orderItems, newStatus);
    }
}