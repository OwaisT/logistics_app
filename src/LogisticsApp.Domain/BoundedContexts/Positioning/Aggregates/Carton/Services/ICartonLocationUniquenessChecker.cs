using LogisticsApp.Domain.BoundedContexts.Positioning.Aggregates.Warehouse.ValueObjects;

namespace LogisticsApp.Domain.BoundedContexts.Positioning.Aggregates.Carton.Services;

public interface ICartonLocationUniquenessChecker
{
    bool IsLocationUnique(WarehouseId warehouseId, RoomId roomId, int onLeft, int below, int behind);
}