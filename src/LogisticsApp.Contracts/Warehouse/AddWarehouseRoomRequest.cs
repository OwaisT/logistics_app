namespace LogisticsApp.Contracts.Warehouse;

public record AddWarehouseRoomRequest(
    Guid WarehouseId,
    string RoomName
);