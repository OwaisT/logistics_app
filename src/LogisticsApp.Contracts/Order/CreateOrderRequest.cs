namespace LogisticsApp.Contracts.Order;

public record CreateOrderRequest(
    decimal TotalValue,
    List<OrderItemRequest> Items
);

public record OrderItemRequest(
    string ProductId,
    string VariationId,
    int Quantity
);
