namespace LogisticsApp.Contracts.Aggregates.Product.Requests.Modifications;

public record ModifyProductAssortmentsRequest(
    List<AssortmentRequest> Assortments);