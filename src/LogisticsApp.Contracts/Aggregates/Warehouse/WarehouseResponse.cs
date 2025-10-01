namespace LogisticsApp.Contracts.Aggregates.Warehouse;

public record WarehouseResponse(
    Guid WarehouseId,
    string Name,
    string Street,
    string Area,
    string City,
    string Postcode,
    string Country,
    DateTime CreatedAt,
    DateTime UpdatedAt,
    bool IsActive,
    List<RoomResponse> Rooms);

public record RoomResponse(
    Guid Id,
    string Name);