namespace LogisticsApp.Contracts.Aggregates.Product.Requests.Modifications.Variations;

public record AddProductVariationsRequest(
    List<Dictionary<string, string>> ColorSizeCombinations);