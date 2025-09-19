namespace LogisticsApp.Contracts.Product;

public record ProductResponse(
    Guid ProductId,
    string RefCode,
    string Season,
    string Name,
    string Description,
    decimal GeneralPrice,
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
    Guid VariationId,
    string ProductRefCode,
    string ProductSeason,
    string Name,
    string Description,
    decimal Price,
    string Color,
    string Size,
    DateTime CreatedAt,
    DateTime UpdatedAt);