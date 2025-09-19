using LogisticsApp.Domain.Aggregates.Carton.ValueObjects;
using LogisticsApp.Domain.Aggregates.Product.ValueObjects;
using LogisticsApp.Domain.Aggregates.Warehouse.ValueObjects;
using LogisticsApp.Domain.Common.Models;

namespace LogisticsApp.Domain.Aggregates.Carton;

public sealed class Carton(CartonId id) : AggregateRoot<CartonId, Guid>(id)
{

    private List<CartonItem> _items = [];
    public CartonLocation? Location { get; private set; }
    public IReadOnlyList<CartonItem> Items => _items.AsReadOnly();

    public void SetLocation(WarehouseId warehouseId, RoomId roomId, int onLeft, int below, int behind)
    {
        Location = CartonLocation.Create(warehouseId, roomId, onLeft, below, behind);
    }

    public void AddItem(ProductId productId, VariationId variationId, string refCode, int quantity)
    {
        var item = new CartonItem(productId, variationId, refCode, quantity);
        _items.Add(item);
        // TODO: increase variation quantity in inventory
    }

    public void RemoveItem(ProductId productId, VariationId variationId, int quantityToRemove)
    {
        // TODO: implement specifications to check if item exists and has enough quantity
        // TODO: decrease variation quantity in inventory
    }

}
