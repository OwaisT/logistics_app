using LogisticsApp.Domain.Aggregates.Warehouse.ValueObjects;
using LogisticsApp.Domain.Common.Exceptions;
using LogisticsApp.Domain.Common.Models;

namespace LogisticsApp.Domain.Aggregates.Warehouse.Entities;

public sealed class Room : Entity<RoomId>
{
    public WarehouseId WarehouseId { get; private set; }
    public string UniqueNumber { get; private set; }
    public int TotalCartons { get; private set; }

    private Room(
        RoomId id,
        WarehouseId warehouseId,
        string uniqueNumber)
        : base(id)
    {
        WarehouseId = warehouseId;
        UniqueNumber = uniqueNumber;
    }

    public static Room Create( WarehouseId warehouseId, string uniqueNumber)
    {
        var id = RoomId.Create(warehouseId, uniqueNumber);
        if (warehouseId == null) throw new CannotBeEmptyException(nameof(warehouseId));
        if (string.IsNullOrWhiteSpace(uniqueNumber)) throw new CannotBeEmptyException(nameof(uniqueNumber));
        return new Room(id, warehouseId, uniqueNumber);
    }

    public void AddCarton()
    {
        TotalCartons++;
    }
    public void RemoveCarton()
    {
        if (TotalCartons <= 0) throw new InvalidOperationException("No cartons to remove.");
        TotalCartons--;
    }
}
