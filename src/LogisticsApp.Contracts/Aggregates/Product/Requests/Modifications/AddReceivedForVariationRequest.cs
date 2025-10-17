namespace LogisticsApp.Contracts.Aggregates.Product.Requests.Modifications;

public record AddReceivedForVariationRequest(
    string Color,
    int ColorQuantity
);
