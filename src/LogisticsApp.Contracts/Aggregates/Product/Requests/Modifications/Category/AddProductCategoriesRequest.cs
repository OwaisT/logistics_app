namespace LogisticsApp.Contracts.Aggregates.Product.Requests.Modifications.Category;

public record AddProductCategoriesRequest(
    List<string> Categories);