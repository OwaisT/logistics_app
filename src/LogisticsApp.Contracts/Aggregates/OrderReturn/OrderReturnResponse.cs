namespace LogisticsApp.Contracts.Aggregates.OrderReturn;

public record OrderReturnResponse(
    Guid Id,
    Guid OrderId,
    List<OrderReturnItemResponse> Items,
    string Status
);

public record OrderReturnItemResponse(
    Guid Id,
    Guid OrderItemId,
    Guid ProductId,
    Guid VariationId,
    string RefCode,
    string Status
);