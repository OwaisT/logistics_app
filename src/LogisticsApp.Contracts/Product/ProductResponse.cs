namespace LogisticsApp.Contracts.Product;

public record ProductResponse(
    Guid ProductId,
    string RefCode,
    string Season,
    string Name,
    string Description,
    DateTime CreatedAt,
    DateTime UpdatedAt,
    bool IsActive,
    List<string> Categories,
    List<string> Colors,
    List<string> Sizes,
    List<AssortmentResponse> Assortments,
    List<VariationResponse> Variations);

public record AssortmentResponse(
    string Color,
    Dictionary<string, int> Sizes);

public record VariationResponse(
    string Id,
    string Name,
    decimal Price,
    int Stock);