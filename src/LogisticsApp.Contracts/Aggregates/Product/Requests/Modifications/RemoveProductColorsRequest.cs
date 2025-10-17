namespace LogisticsApp.Contracts.Aggregates.Product.Requests.Modifications;

public record RemoveProductColorsRequest(
    List<string> ColorsToRemove);