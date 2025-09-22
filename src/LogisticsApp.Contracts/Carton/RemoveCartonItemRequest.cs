namespace LogisticsApp.Contracts.Carton;

public record RemoveCartonItemRequest(
    string ProductId,
    string VariationId,
    int Quantity);