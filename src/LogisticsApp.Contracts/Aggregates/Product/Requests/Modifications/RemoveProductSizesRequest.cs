namespace LogisticsApp.Contracts.Aggregates.Product.Requests.Modifications;

public record RemoveProductSizesRequest(
    List<string> SizesToRemove);