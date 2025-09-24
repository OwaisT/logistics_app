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

    public Warehouse? GetByDetails(string name, string street, string area, string city, string postcode, string country)
    {
        return _warehouses.FirstOrDefault(w =>
            w.Name.Equals(name, StringComparison.OrdinalIgnoreCase) &&
            w.Street.Equals(street, StringComparison.OrdinalIgnoreCase) &&
            w.Area.Equals(area, StringComparison.OrdinalIgnoreCase) &&
            w.City.Equals(city, StringComparison.OrdinalIgnoreCase) &&
            w.Postcode.Equals(postcode, StringComparison.OrdinalIgnoreCase) &&
            w.Country.Equals(country, StringComparison.OrdinalIgnoreCase)
        );
    }

}