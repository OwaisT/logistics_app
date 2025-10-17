namespace LogisticsApp.Contracts.Aggregates.Product.Requests.Modifications.Size;

public record RemoveProductSizesRequest(
    List<string> SizesToRemove);