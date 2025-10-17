namespace LogisticsApp.Contracts.Aggregates.Product.Requests.Modifications;

public record RemoveProductCategoriesRequest(
    List<string> Categories);