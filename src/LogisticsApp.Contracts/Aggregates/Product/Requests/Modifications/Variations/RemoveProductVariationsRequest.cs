namespace LogisticsApp.Contracts.Aggregates.Product.Requests.Modifications.Variations;

public record RemoveProductVariationsRequest(
    List<string> VariationIds);