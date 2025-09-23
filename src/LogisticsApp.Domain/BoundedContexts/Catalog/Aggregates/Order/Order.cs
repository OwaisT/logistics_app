using LogisticsApp.Domain.BoundedContexts.Catalog.Aggregates.Order.ValueObjects;
using LogisticsApp.Domain.Common.Models;

namespace LogisticsApp.Domain.BoundedContexts.Catalog.Aggregates.Order;

public sealed class Order : AggregateRoot<OrderId, Guid>
{
    private List<OrderItem> _items = null!;
    public IReadOnlyList<OrderItem> Items => _items.AsReadOnly();
    public int TotalItems => _items.Sum(i => i.Quantity);
    public decimal TotalPrice { get; private set; }
    public string Status { get; private set; } = "Pending";

    private Order(
        OrderId id,
        List<OrderItem> items,
        decimal totalPrice,
        string status)
         : base(id)
    {
        _items = items;
        TotalPrice = totalPrice;
        Status = status;
    }

    public static Order Create(
        List<OrderItem> items,
        decimal totalPrice,
        string status)
    {
        return new Order(
            OrderId.CreateUnique(),
            items,
            totalPrice,
            status);
    }

}