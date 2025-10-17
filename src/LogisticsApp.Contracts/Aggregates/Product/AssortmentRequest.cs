namespace LogisticsApp.Contracts.Aggregates.Product;

public record AssortmentRequest(
    string Color,
    Dictionary<string, int> Sizes);