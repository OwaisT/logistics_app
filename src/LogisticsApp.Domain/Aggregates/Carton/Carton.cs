using LogisticsApp.Domain.Aggregates.Carton.Events;
using LogisticsApp.Domain.Aggregates.Carton.ValueObjects;
using LogisticsApp.Domain.Aggregates.Product.ValueObjects;
using LogisticsApp.Domain.Aggregates.Warehouse.ValueObjects;
using LogisticsApp.Domain.Common.Models;

namespace LogisticsApp.Domain.Aggregates.Carton;

public sealed class Carton : AggregateRoot<CartonId, Guid>
{

    private List<CartonItem> _items = [];
    public CartonLocation? Location { get; private set; }
    public IReadOnlyList<CartonItem> Items => _items.AsReadOnly();

    private Carton(CartonId id) : base(id)
    {
    }

    public static Carton Create()
    {
        var cartonId = CartonId.CreateUnique();
        return new Carton(cartonId);
    }

    public void SetLocation(WarehouseId warehouseId, string warehouseName, RoomId roomId, string roomName, int onLeft, int below, int behind)
    {
        Location = CartonLocation.Create(warehouseId, warehouseName, roomId, roomName, onLeft, below, behind);
    }

    public void AddItem(ProductId productId, VariationId variationId, string refCode, int quantity)
    {
        if (quantity <= 0)
        {
            throw new ArgumentException("Quantity must be greater than zero.", nameof(quantity));
        }
        if (_items.Any(i => i.ProductId == productId && i.VariationId == variationId))
        {
            var existingItem = _items.First(i => i.ProductId == productId && i.VariationId == variationId);
            _items.Remove(existingItem);
            var updatedItem = new CartonItem(productId, variationId, refCode, existingItem.Quantity + quantity);
            _items.Add(updatedItem);
            return;
        }
        var item = new CartonItem(productId, variationId, refCode, quantity);
        _items.Add(item);

        AddDomainEvent(new CartonItemAdded(this.Id.Value, productId.Value, variationId.Value, quantity));
        // TODO: increase variation quantity in inventory
    }

    public void RemoveItem(ProductId productId, VariationId variationId, int quantityToRemove)
    {
        // TODO: implement specifications to check if item exists and has enough quantity
        // TODO: decrease variation quantity in inventory
    }

}
