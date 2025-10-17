namespace LogisticsApp.Contracts.Aggregates.Product.Requests.Modifications.Color;

public record RemoveProductColorsRequest(
    List<string> ColorsToRemove);