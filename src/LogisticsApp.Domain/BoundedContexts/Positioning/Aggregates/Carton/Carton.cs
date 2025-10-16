using ErrorOr;
using LogisticsApp.Domain.BoundedContexts.Catalog.Aggregates.ProductAggregate.ValueObjects;
using LogisticsApp.Domain.BoundedContexts.Positioning.Aggregates.Carton.Events;
using LogisticsApp.Domain.BoundedContexts.Positioning.Aggregates.Carton.ValueObjects;
using LogisticsApp.Domain.BoundedContexts.Positioning.Aggregates.Warehouse.ValueObjects;
using LogisticsApp.Domain.Common.Errors;
using LogisticsApp.Domain.Common.Models;

namespace LogisticsApp.Domain.BoundedContexts.Positioning.Aggregates.Carton;

public sealed class Carton : AggregateRoot<CartonId, Guid>
{
    private List<CartonItem> _items = [];
    public CartonLocation? Location { get; private set; }
    public IReadOnlyList<CartonItem> Items => _items.AsReadOnly();

    private Carton(CartonId id) : base(id)
    {
        Location = CartonLocation.Create(WarehouseId.Create(Guid.Empty), "Unassigned", RoomId.Create(Guid.Empty), "Unassigned", 0, 0, 0);
    }

    public static Carton Create()
    {
        var cartonId = CartonId.CreateUnique();
        return new Carton(cartonId);
    }

    internal ErrorOr<Carton> SetLocation(WarehouseId warehouseId, string warehouseName, RoomId roomId, string roomName, int onLeft, int below, int behind)
    {
        Location = CartonLocation.Create(warehouseId, warehouseName, roomId, roomName, onLeft, below, behind);
        return this;
    }

    internal ErrorOr<Carton> AddItem(ProductId productId, VariationId variationId, string refCode, int quantity)
    {
        // If item already exists, update quantity
        if (_items.Any(i => i.ProductId == productId && i.VariationId == variationId))
        {
            var existingItem = _items.First(i => i.ProductId == productId && i.VariationId == variationId);
            _items.Remove(existingItem);
            var updatedItem = CartonItem.Create(productId, variationId, refCode, existingItem.Quantity + quantity);
            _items.Add(updatedItem);
        }
        else
        {
            var item = CartonItem.Create(productId, variationId, refCode, quantity);
            _items.Add(item);
        }
        AddDomainEvent(new CartonItemAdded(Id.Value, productId.Value, variationId.Value, quantity));

        return this;
        // TODO: Check if need to raise domain event for adding item to carton
    }

    public ErrorOr<Carton> RemoveItem(ProductId productId, VariationId variationId, int quantityToRemove)
    {
        if (quantityToRemove <= 0)
        {
            return Errors.Common.CannotBeNegativeOrZero(nameof(quantityToRemove));
        }
        // Check if item exists
        if (!_items.Any(i => i.ProductId == productId && i.VariationId == variationId))
        {
            return Errors.Common.EntityNotFound("CartonItem", $"{productId}-{variationId}");
        }
        var existingItem = _items.First(i => i.ProductId == productId && i.VariationId == variationId);
        if (existingItem.Quantity < quantityToRemove)
        {
            return Errors.Common.CannotBeNegativeOrZero(nameof(quantityToRemove));
        }
        // TODO: assign directly to existing item
        _items.Remove(existingItem);
        var remainingQuantity = existingItem.Quantity - quantityToRemove;
        if (remainingQuantity > 0)
        {
            var updatedItem = CartonItem.Create(productId, variationId, existingItem.RefCode, remainingQuantity);
            _items.Add(updatedItem);
        }
        return this;
        // TODO: Check if need to raise domain event for removing item from carton
    }

#pragma warning disable CS8618
    private Carton() : base(default!) { }
#pragma warning restore CS8618
}
