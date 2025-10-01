namespace LogisticsApp.Contracts.Aggregates.Carton;

public record AssignCartonLocationRequest(
    string WarehouseId,
    string RoomId,
    int OnLeft,
    int Below,
    int Behind
);