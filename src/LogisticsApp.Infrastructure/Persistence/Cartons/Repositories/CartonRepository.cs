using LogisticsApp.Application.Common.Interfaces.Persistence;
using LogisticsApp.Domain.BoundedContexts.Positioning.Aggregates.Carton;
using LogisticsApp.Domain.BoundedContexts.Positioning.Aggregates.Carton.ValueObjects;
using LogisticsApp.Domain.BoundedContexts.Positioning.Aggregates.Warehouse.ValueObjects;

namespace LogisticsApp.Infrastructure.Persistence.Cartons;

public class CartonRepository : ICartonRepository
{
    private static readonly List<Carton> _cartons = [];

    public void Add(Carton carton)
    {
        _cartons.Add(carton);
    }

    public Carton? GetById(CartonId id)
    {
        return _cartons.FirstOrDefault(c => c.Id == id);
    }

    public bool ExistsAtLocation(WarehouseId warehouseId, RoomId roomId, int onLeft, int below, int behind)
    {
        return _cartons.Any(c => c.Location?.WarehouseId == warehouseId &&
                                c.Location?.RoomId == roomId &&
                                c.Location?.OnLeft == onLeft &&
                                c.Location?.Below == below &&
                                c.Location?.Behind == behind);
    }
}