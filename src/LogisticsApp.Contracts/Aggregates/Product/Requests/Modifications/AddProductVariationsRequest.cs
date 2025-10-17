namespace LogisticsApp.Contracts.Aggregates.Product.Requests.Modifications;

public record AddProductVariationsRequest(
    List<string> Colors,
    List<string> Sizes);