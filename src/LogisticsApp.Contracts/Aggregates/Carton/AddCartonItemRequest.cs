namespace LogisticsApp.Contracts.Aggregates.Carton;

public record AddCartonItemRequest(
    string ProductId,
    string VariationId,
    int Quantity);