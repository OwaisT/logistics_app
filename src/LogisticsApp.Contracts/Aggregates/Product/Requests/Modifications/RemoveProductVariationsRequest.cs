namespace LogisticsApp.Contracts.Aggregates.Product.Requests.Modifications;

public record RemoveProductVariationsRequest(
    List<string> VariationIds);