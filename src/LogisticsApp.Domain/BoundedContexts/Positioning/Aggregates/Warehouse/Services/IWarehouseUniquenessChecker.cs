namespace LogisticsApp.Domain.BoundedContexts.Positioning.Aggregates.Warehouse.Services;

public interface IWarehouseUniquenessChecker
{
    bool IsUnique(string name, string street, string area, string city, string postcode, string country);
}
