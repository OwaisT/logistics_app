namespace LogisticsApp.Contracts.Aggregates.Product.Requests.Modifications;

public record ModifyVariationPriceRequest(
    decimal NewPrice,
    string Color);