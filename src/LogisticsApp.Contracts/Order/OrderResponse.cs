namespace LogisticsApp.Contracts.Order;

public record OrderResponse(
    string OrderId,
    decimal TotalValue,
    int TotalItemsCount,
    List<OrderItemResponse> Items,
    string Status
);

public record OrderItemResponse(
    string ProductId,
    string VariationId,
    string RefCode,
    int Quantity
);