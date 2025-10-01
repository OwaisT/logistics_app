using LogisticsApp.Application.Common.Interfaces.Persistence;
using LogisticsApp.Domain.BoundedContexts.Positioning.Aggregates.Warehouse.Services;

namespace LogisticsApp.Application.Aggregates.Warehouses.Services;

public class WarehouseUniquenessChecker : IWarehouseUniquenessChecker
{
    private readonly IWarehouseRepository _warehouseRepository;

    public WarehouseUniquenessChecker(IWarehouseRepository warehouseRepository)
    {
        _warehouseRepository = warehouseRepository;
    }

    public bool IsUnique(string name, string street, string area, string city, string postcode, string country)
    {
        
        var warehouse = _warehouseRepository.GetByDetails(name, street, area, city, postcode, country);
        return warehouse == null;
    }
}