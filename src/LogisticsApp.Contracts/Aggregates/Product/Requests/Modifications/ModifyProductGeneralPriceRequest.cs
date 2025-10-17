namespace LogisticsApp.Contracts.Aggregates.Product.Requests.Modifications;

public record ModifyProductGeneralPriceRequest(
    decimal NewGeneralPrice,
    bool UpdateVariationsPrices);