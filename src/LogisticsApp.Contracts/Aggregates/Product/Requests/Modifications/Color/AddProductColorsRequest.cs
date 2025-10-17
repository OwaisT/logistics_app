namespace LogisticsApp.Contracts.Aggregates.Product.Requests.Modifications.Color;

public record AddProductColorsRequest(
    List<string> Colors);