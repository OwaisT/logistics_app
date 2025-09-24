using LogisticsApp.Application.Common.Interfaces.Persistence;
using LogisticsApp.Domain.BoundedContexts.Positioning.Aggregates.Carton.Services;
using LogisticsApp.Domain.BoundedContexts.Positioning.Aggregates.Warehouse.ValueObjects;

namespace LogisticsApp.Application.Cartons.Services;

public class CartonLocationUniquenessChecker : ICartonLocationUniquenessChecker
{
    private readonly ICartonRepository _cartonRepository;

    public CartonLocationUniquenessChecker(ICartonRepository cartonRepository)
    {
        _cartonRepository = cartonRepository;
    }

    public bool IsLocationUnique(WarehouseId warehouseId, RoomId roomId, int onLeft, int below, int behind)
    {
        return !_cartonRepository.ExistsAtLocation(warehouseId, roomId, onLeft, below, behind);
    }
}
