namespace LogisticsApp.Contracts.Aggregates.Product.Requests.Modifications;

public record AddProductSizesRequest(
    List<string> Sizes);