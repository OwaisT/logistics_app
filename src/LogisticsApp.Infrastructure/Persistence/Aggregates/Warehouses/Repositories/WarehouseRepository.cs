using LogisticsApp.Application.Common.Interfaces.Persistence;
using LogisticsApp.Domain.BoundedContexts.Positioning.Aggregates.Warehouse;
using LogisticsApp.Domain.BoundedContexts.Positioning.Aggregates.Warehouse.ValueObjects;
using Microsoft.EntityFrameworkCore;

namespace LogisticsApp.Infrastructure.Persistence.Aggregates.Warehouses.Repositories;

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
        return _dbContext.Warehouses.FirstOrDefault(w =>
            EF.Functions.ILike(w.Name, name) &&
            EF.Functions.ILike(w.Street, street) &&
            EF.Functions.ILike(w.Area, area) &&
            EF.Functions.ILike(w.City, city) &&
            EF.Functions.ILike(w.Postcode, postcode) &&
            EF.Functions.ILike(w.Country, country)
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