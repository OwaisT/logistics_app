namespace LogisticsApp.Contracts.Aggregates.Product.Requests.Modifications.Variations;

public record ModifyVariationsPriceRequest(
    decimal NewPrice,
    string Color);