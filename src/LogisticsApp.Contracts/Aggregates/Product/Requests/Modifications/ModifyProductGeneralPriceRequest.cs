namespace LogisticsApp.Contracts.Aggregates.Product.Requests.Modifications;

public record ModifyProductGeneralPriceRequest(
    decimal NewPrice,
    bool UpdateVariationsPrices);