using LogisticsApp.Domain.Aggregates.Product.ValueObjects;
using LogisticsApp.Domain.Aggregates.Warehouse.Exceptions;
using LogisticsApp.Domain.Aggregates.Warehouse.ValueObjects;
using LogisticsApp.Domain.Common.Models;

namespace LogisticsApp.Domain.Aggregates.Warehouse.Entities;

public sealed class Carton : Entity<CartonId>
{
    public Location Location { get; private set; }

    public List<CartonItem> CartonItems { get; } = [];

    public int TotalItems => CartonItems.Sum(ci => ci.Quantity);

    private Carton(
        CartonId id,
        Location location)
        : base(id)
    {
        Location = location;
    }

    public static Carton Create(Location location)
    {
        return new Carton(CartonId.CreateUnique(), location);
    }

    public void UpdateLocation(Location location)
    {
        Location = location;
    }

    public void AddCartonItem(VariationId variationId, int quantity)
    {
        var existingItem = CartonItems.FirstOrDefault(ci => ci.VariationId == variationId);
        if (existingItem != null)
        {
            existingItem.IncreaseQuantity(quantity);
        }
        else
        {
            var newItem = CartonItem.Create(variationId, quantity);
            CartonItems.Add(newItem);
        }
    }

    public void RemoveCartonItem(VariationId variationId, int quantity)
    {
        var existingItem = CartonItems.FirstOrDefault(ci => ci.VariationId == variationId);
        if (existingItem == null)
        {
            throw new CartonItemNotFoundException(variationId);
        }

        existingItem.DecreaseQuantity(quantity);

        if (existingItem.Quantity == 0)
        {
            CartonItems.Remove(existingItem);
        }

    }
    
}
