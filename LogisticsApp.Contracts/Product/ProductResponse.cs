namespace LogisticsApp.Contracts.Product;

public record ProductResponse(
    string ProductId,
    string RefCode,
    string Season,
    string Name,
    string Description,
    DateTime CreatedAt,
    DateTime UpdatedAt,
    bool IsActive,
    List<string> Categories,
    List<string> Colors,
    List<string> Sizes);