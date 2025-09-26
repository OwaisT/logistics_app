using LogisticsApp.Application.Common.Interfaces.Persistence;
using LogisticsApp.Domain.BoundedContexts.Positioning.Aggregates.Warehouse;
using LogisticsApp.Domain.BoundedContexts.Positioning.Aggregates.Warehouse.ValueObjects;

namespace LogisticsApp.Infrastructure.Persistence.Warehouses.Repositories;

public class WarehouseRepository : IWarehouseRepository
{
    private static readonly List<Warehouse> _warehouses = [];

    private readonly LogisticsAppDbContext _dbContext;

    public WarehouseRepository(LogisticsAppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    // Create
    public void Add(Warehouse warehouse)
    {
        _dbContext.Add(warehouse);
        _dbContext.SaveChanges();
    }

    // Read
    public Warehouse? GetById(WarehouseId id)
    {
        return _dbContext.Warehouses.FirstOrDefault(w => w.Id == id);
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

    // Update
    public void Update(Warehouse warehouse)
    {
        _dbContext.Update(warehouse);
        _dbContext.SaveChanges();
    }

    // Delete
    public void Delete(Warehouse warehouse)
    {
        _dbContext.Remove(warehouse);
        _dbContext.SaveChanges();
    }

}