using LogisticsApp.Domain.BoundedContexts.Positioning.Aggregates.Warehouse;

namespace LogisticsApp.Application.Common.Interfaces.Persistence;

public interface IWarehouseRepository
{
    // Define methods for warehouse data access, e.g.:
    // Task<Warehouse> GetByIdAsync(string id);
    // Task SaveAsync(Warehouse warehouse);
    void Add(Warehouse warehouse);

    Warehouse? GetById(Guid id);

    Warehouse? GetByDetails(string name, string street, string area, string city, string postcode, string country);

}