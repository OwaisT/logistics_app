namespace LogisticsApp.Contracts.Aggregates.Product.Requests.Modifications.Size;

public record AddProductSizesRequest(
    List<string> Sizes,
    List<AssortmentRequest> Assortments);