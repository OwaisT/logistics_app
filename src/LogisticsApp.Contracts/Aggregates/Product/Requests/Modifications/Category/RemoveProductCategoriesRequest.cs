namespace LogisticsApp.Contracts.Aggregates.Product.Requests.Modifications.Category;

public record RemoveProductCategoriesRequest(
    List<string> Categories);