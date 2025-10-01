namespace LogisticsApp.Contracts.Aggregates.Carton;

public record RemoveCartonItemRequest(
    string ProductId,
    string VariationId,
    int Quantity);