using LogisticsApp.Domain.Aggregates.Warehouse.ValueObjects;
using LogisticsApp.Domain.Common.Models;

namespace LogisticsApp.Domain.Aggregates.Carton.ValueObjects;

public class CartonLocation : ValueObject
{
    public WarehouseId WarehouseId { get; private set; }
    public RoomId RoomId { get; private set; }
    public int OnLeft { get; private set; }
    public int Below { get; private set; }
    public int Behind { get; private set; }

    private CartonLocation(WarehouseId warehouseId, RoomId roomId, int onLeft, int below, int behind)
    {
        WarehouseId = warehouseId;
        RoomId = roomId;
        OnLeft = onLeft;
        Below = below;
        Behind = behind;
    }

    public static CartonLocation Create(WarehouseId warehouseId, RoomId roomId, int onLeft, int below, int behind)
    {
        // Add any necessary validation here
        return new CartonLocation(warehouseId, roomId, onLeft, below, behind);
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