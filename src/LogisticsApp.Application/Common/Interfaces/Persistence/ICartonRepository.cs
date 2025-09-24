using LogisticsApp.Domain.BoundedContexts.Positioning.Aggregates.Carton;
using LogisticsApp.Domain.BoundedContexts.Positioning.Aggregates.Carton.ValueObjects;
using LogisticsApp.Domain.BoundedContexts.Positioning.Aggregates.Warehouse.ValueObjects;

namespace LogisticsApp.Application.Common.Interfaces.Persistence;

public interface ICartonRepository
{
    // Define methods for carton data access, e.g.:
    // Task<Carton> GetByIdAsync(string id);
    // Task SaveAsync(Carton carton);
    void Add(Carton carton);

    Carton? GetById(CartonId id);

    bool ExistsAtLocation(WarehouseId warehouseId, RoomId roomId, int onLeft, int below, int behind);

}