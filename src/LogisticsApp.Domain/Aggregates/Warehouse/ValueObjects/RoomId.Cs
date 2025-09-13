using LogisticsApp.Domain.Common.Models;

namespace LogisticsApp.Domain.Aggregates.Warehouse.ValueObjects;

public sealed class RoomId : ValueObject
{
    private WarehouseId WarehouseId { get; }
    private string UniqueNumber { get; }
    public string WarehouseCode => $"{WarehouseId}-RM-{UniqueNumber}";

    private RoomId(WarehouseId warehouseId, string uniqueNumber)
    {
        WarehouseId = warehouseId;
        UniqueNumber = uniqueNumber;
    }

    public static RoomId Create(WarehouseId warehouseId, string uniqueNumber)
    {
        return new RoomId(warehouseId, uniqueNumber);
    }
    public override IEnumerable<object> GetEqualityComponents()
    {
        yield return WarehouseId;
        yield return UniqueNumber;
    }
}
