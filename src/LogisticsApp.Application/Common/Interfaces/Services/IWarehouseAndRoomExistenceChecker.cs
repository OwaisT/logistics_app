using ErrorOr;
using LogisticsApp.Domain.BoundedContexts.Positioning.Aggregates.Warehouse.ValueObjects;

namespace LogisticsApp.Application.Common.Interfaces.Services;

public interface IWarehouseAndRoomExistenceChecker
{
    public ErrorOr<Dictionary<string, string>> CheckExistenceAndGetNames(WarehouseId warehouseId, RoomId roomId);
}