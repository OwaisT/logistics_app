namespace LogisticsApp.Contracts.Carton;

public record CartonResponse(
    Guid CartonId,
    CartonLocationResponse? Location,
    List<CartonItemResponse> Items);

public record CartonLocationResponse(
    Guid WarehouseId,
    string WarehouseName,
    Guid RoomId,
    string RoomName,
    int OnLeft,
    int Below,
    int Behind);

public record CartonItemResponse(
    Guid ProductId,
    Guid VariationId,
    string RefCode,
    int Quantity);