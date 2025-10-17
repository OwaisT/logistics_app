namespace LogisticsApp.Contracts.Aggregates.Product.Requests.Modifications.Variations;

public record AddReceivedForVariationRequest(
    string Color,
    int ColorQuantity
);
