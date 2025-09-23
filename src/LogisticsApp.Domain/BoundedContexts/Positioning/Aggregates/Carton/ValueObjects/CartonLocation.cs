using LogisticsApp.Domain.BoundedContexts.Positioning.Aggregates.Warehouse.ValueObjects;
using LogisticsApp.Domain.Common.Models;

namespace LogisticsApp.Domain.BoundedContexts.Positioning.Aggregates.Carton.ValueObjects;

public sealed class CartonLocation : ValueObject
{
    public WarehouseId WarehouseId { get; private set; }
    public string WarehouseName { get; private set; }
    public RoomId RoomId { get; private set; }
    public string RoomName { get; private set; }
    public int OnLeft { get; private set; }
    public int Below { get; private set; }
    public int Behind { get; private set; }

    private CartonLocation(WarehouseId warehouseId, string warehouseName, RoomId roomId, string roomName, int onLeft, int below, int behind)
    {
        WarehouseId = warehouseId;
        WarehouseName = warehouseName;
        RoomId = roomId;
        RoomName = roomName;
        OnLeft = onLeft;
        Below = below;
        Behind = behind;
    }

    public static CartonLocation Create(WarehouseId warehouseId, string warehouseName, RoomId roomId, string roomName, int onLeft, int below, int behind)
    {
        // Add any necessary validation here
        return new CartonLocation(warehouseId, warehouseName, roomId, roomName, onLeft, below, behind);
    }

    public override IEnumerable<object> GetEqualityComponents()
    {
        yield return WarehouseId;
        yield return RoomId;
        yield return OnLeft;
        yield return Below;
        yield return Behind;
    }
}