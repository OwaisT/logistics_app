namespace LogisticsApp.Contracts.Carton;

public record AddCartonItemRequest(
    string ProductId,
    string VariationId,
    int Quantity);