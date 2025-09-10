namespace LogisticsApp.Contracts.Product;

public record CreateProductRequest(
    string RefCode, 
    string Season,
    string Name, 
    string Description,
    bool IsActive,
    List<string> Categories,
    List<string> Colors,
    List<string> Sizes,
    List<Assortment> Assortments);

public record Assortment(
    string Color,
    Dictionary<string, int> Sizes);