using ErrorOr;
using LogisticsApp.Domain.BoundedContexts.Catalog.Aggregates.Product.ValueObjects;
using LogisticsApp.Domain.BoundedContexts.Positioning.Aggregates.Carton.Events;
using LogisticsApp.Domain.BoundedContexts.Positioning.Aggregates.Carton.Services;
using LogisticsApp.Domain.BoundedContexts.Positioning.Aggregates.Carton.ValueObjects;
using LogisticsApp.Domain.BoundedContexts.Positioning.Aggregates.Warehouse.ValueObjects;
using LogisticsApp.Domain.Common.Errors;
using LogisticsApp.Domain.Common.Models;

namespace LogisticsApp.Domain.BoundedContexts.Positioning.Aggregates.Carton;

public sealed class Carton : AggregateRoot<CartonId, Guid>
{
    private readonly ICartonLocationUniquenessChecker _locationUniquenessChecker;

    private List<CartonItem> _items = [];
    public CartonLocation? Location { get; private set; }
    public IReadOnlyList<CartonItem> Items => _items.AsReadOnly();

    private Carton(CartonId id, ICartonLocationUniquenessChecker locationUniquenessChecker) : base(id)
    {
        _locationUniquenessChecker = locationUniquenessChecker;
        Location = null;
    }

    public static Carton Create(ICartonLocationUniquenessChecker locationUniquenessChecker)
    {
        var cartonId = CartonId.CreateUnique();
        return new Carton(cartonId, locationUniquenessChecker);
    }

    public ErrorOr<Carton> SetLocation(WarehouseId warehouseId, string warehouseName, RoomId roomId, string roomName, int onLeft, int below, int behind)
    {
        if (_locationUniquenessChecker.IsLocationUnique(warehouseId, roomId, onLeft, below, behind) == false)
        {
            return Errors.Carton.LocationNotUnique;
        }
        Location = CartonLocation.Create(warehouseId, warehouseName, roomId, roomName, onLeft, below, behind);
        return this;
    }

    public ErrorOr<Carton> AddItem(ProductId productId, VariationId variationId, string refCode, int quantity)
    {
        if (quantity <= 0)
        {
            return Errors.Common.CannotBeNegativeOrZero(nameof(quantity));
        }
        // If item already exists, update quantity
        if (_items.Any(i => i.ProductId == productId && i.VariationId == variationId))
        {
            var existingItem = _items.First(i => i.ProductId == productId && i.VariationId == variationId);
            _items.Remove(existingItem);
            var updatedItem = new CartonItem(productId, variationId, refCode, existingItem.Quantity + quantity);
            _items.Add(updatedItem);
        }
        else
        {
            var item = new CartonItem(productId, variationId, refCode, quantity);
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
        _items.Remove(existingItem);
        var remainingQuantity = existingItem.Quantity - quantityToRemove;
        if (remainingQuantity > 0)
        {
            var updatedItem = new CartonItem(productId, variationId, existingItem.RefCode, remainingQuantity);
            _items.Add(updatedItem);
        }
        return this;
        // TODO: Check if need to raise domain event for removing item from carton
    }

}
