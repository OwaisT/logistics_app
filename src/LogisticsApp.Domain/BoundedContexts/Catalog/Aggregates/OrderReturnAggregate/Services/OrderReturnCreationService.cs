using ErrorOr;
using LogisticsApp.Domain.BoundedContexts.Catalog.Aggregates.OrderAggregate.Entities;
using LogisticsApp.Domain.BoundedContexts.Catalog.Aggregates.OrderAggregate.ValueObjects;
using LogisticsApp.Domain.BoundedContexts.Catalog.Aggregates.OrderReturnAggregate.Entities;

namespace LogisticsApp.Domain.BoundedContexts.Catalog.Aggregates.OrderReturnAggregate.Services;

public class OrderReturnCreationService(
    IOrderReturnItemsValidation _orderReturnItemsValidation)
{

    public ErrorOr<OrderReturn> CreateOrderReturn(OrderId orderId, List<Guid> orderItemIds)
    {
        if (orderItemIds == null || orderItemIds.Count == 0)
        {
            // TODO: Create a proper error type for this
            return Error.Validation(code: "OrderReturn.Items.Empty", description: "Order return must contain at least one item.");
        }

        var areItemsValidResult = _orderReturnItemsValidation.ValidateAndGetOrderItemsForReturn(orderId, orderItemIds);
        if (areItemsValidResult.IsError)
        {
            return areItemsValidResult.FirstError;
        }
        var orderItems = areItemsValidResult.Value;

        List<OrderReturnItem> orderReturnItems = [.. orderItems.Select(oi => OrderReturnItem.Create(
            oi.Id,
            oi.ProductId,
            oi.VariationId,
            oi.RefCode
        ))];

        var orderReturn = OrderReturn.Create(orderId, orderReturnItems, "Pending");
        return orderReturn;
    }
    
}