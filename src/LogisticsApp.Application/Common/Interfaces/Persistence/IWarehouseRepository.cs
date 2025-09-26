using LogisticsApp.Domain.BoundedContexts.Positioning.Aggregates.Warehouse;
using LogisticsApp.Domain.BoundedContexts.Positioning.Aggregates.Warehouse.ValueObjects;

namespace LogisticsApp.Application.Common.Interfaces.Persistence;

public interface IWarehouseRepository
{
    // Define methods for warehouse data access, e.g.:
    // Task<Warehouse> GetByIdAsync(string id);
    // Task SaveAsync(Warehouse warehouse);
    void Add(Warehouse warehouse);

    Warehouse? GetById(WarehouseId id);

    Warehouse? GetByDetails(string name, string street, string area, string city, string postcode, string country);

    void Update(Warehouse warehouse);
    void Delete(Warehouse warehouse);

}