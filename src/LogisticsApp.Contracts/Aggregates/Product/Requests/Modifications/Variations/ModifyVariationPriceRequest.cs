namespace LogisticsApp.Contracts.Aggregates.Product.Requests.Modifications.Variations;

public record ModifyVariationPriceRequest(
    decimal NewPrice,
    string Color);