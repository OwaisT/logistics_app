namespace LogisticsApp.Contracts.Aggregates.Product.Requests.Modifications.Variations;

public record AddProductVariationsRequest(
    List<string> Colors,
    List<string> Sizes);