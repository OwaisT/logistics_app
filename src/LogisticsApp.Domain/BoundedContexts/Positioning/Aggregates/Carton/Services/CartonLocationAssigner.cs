using ErrorOr;
using LogisticsApp.Domain.BoundedContexts.Positioning.Aggregates.Warehouse.ValueObjects;
using LogisticsApp.Domain.Common.Errors;

namespace LogisticsApp.Domain.BoundedContexts.Positioning.Aggregates.Carton.Services;

public class CartonLocationAssigner(
    ICartonLocationUniquenessChecker _locationUniquenessChecker)
{
    public ErrorOr<Carton> AssignLocationToCarton(
        Carton carton,
        WarehouseId warehouseId,
        string warehouseName,
        RoomId roomId,
        string roomName,
        int onLeft,
        int below,
        int behind)
    {
        if (!_locationUniquenessChecker.IsLocationUnique(warehouseId, roomId, onLeft, below, behind))
        {
            return Errors.Carton.LocationNotUnique;
        }
        return carton.SetLocation(warehouseId, warehouseName, roomId, roomName, onLeft, below, behind);
    }
}
