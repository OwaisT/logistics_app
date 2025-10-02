namespace LogisticsApp.Contracts.Aggregates.Order;
public record UpdateOrderItemsStatusRequest(
    List<Guid> OrderItemsIds,
    string Status
);