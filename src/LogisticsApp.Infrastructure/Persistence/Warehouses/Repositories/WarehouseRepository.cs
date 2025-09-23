using LogisticsApp.Application.Common.Interfaces.Persistence;
using LogisticsApp.Domain.BoundedContexts.Positioning.Aggregates.Warehouse;

namespace LogisticsApp.Infrastructure.Persistence.Repositories.Warehouses;

public class WarehouseRepository : IWarehouseRepository
{
    private static readonly List<Warehouse> _warehouses = [];

    public void Add(Warehouse warehouse)
    {
        _warehouses.Add(warehouse);
    }

    public Warehouse? GetById(Guid id)
    {
        return _warehouses.FirstOrDefault(w => w.Id.Value == id);
    }

}