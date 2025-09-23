using LogisticsApp.Domain.BoundedContexts.Positioning.Aggregates.Warehouse.ValueObjects;

namespace LogisticsApp.Domain.BoundedContexts.Positioning.Aggregates.Carton.Services;

public interface ICartonLocationUniquenessChecker
{
    // TODO: create an implementation that checks the database for existing locations
    bool IsLocationUnique(WarehouseId warehouseId, RoomId roomId, int onLeft, int below, int behind);
}