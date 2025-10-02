namespace LogisticsApp.Contracts.Aggregates.OrderReturn;

public record CreateOrderReturnRequest(
    Guid OrderId,
    List<Guid> OrderItemIds
);