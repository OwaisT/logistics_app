namespace LogisticsApp.Contracts.Aggregates.Product;

public record CreateProductRequest(
    string RefCode, 
    string Season,
    string Name, 
    string Description,
    decimal GeneralPrice,
    bool IsActive,
    List<string> Categories,
    List<string> Colors,
    List<string> Sizes,
    List<AssortmentRequest> Assortments);
