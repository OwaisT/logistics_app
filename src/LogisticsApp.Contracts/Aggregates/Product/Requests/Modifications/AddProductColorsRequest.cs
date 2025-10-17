namespace LogisticsApp.Contracts.Aggregates.Product.Requests.Modifications;

public record AddProductColorsRequest(
    List<string> Colors);