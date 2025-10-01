namespace LogisticsApp.Contracts.Product;

public record AddReceivedForVariationRequest(
    string Color,
    int ColorQuantity
);
