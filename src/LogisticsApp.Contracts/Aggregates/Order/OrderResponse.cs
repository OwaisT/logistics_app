namespace LogisticsApp.Contracts.Aggregates.Order;

public record OrderResponse(
    Guid OrderId,
    decimal TotalValue,
    int TotalItemsCount,
    List<OrderItemResponse> Items,
    string Status
);

public record OrderItemResponse(
    Guid ProductId,
    Guid VariationId,
    string RefCode,
    int Quantity,
    string Status
);