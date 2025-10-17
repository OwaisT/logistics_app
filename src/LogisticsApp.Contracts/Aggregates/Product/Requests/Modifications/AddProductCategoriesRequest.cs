namespace LogisticsApp.Contracts.Aggregates.Product.Requests.Modifications;

public record AddProductCategoriesRequest(
    List<string> Categories);