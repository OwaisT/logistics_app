namespace LogisticsApp.Contracts.Aggregates.Product;

public record AddReceivedForVariationRequest(
    string Color,
    int ColorQuantity
);
