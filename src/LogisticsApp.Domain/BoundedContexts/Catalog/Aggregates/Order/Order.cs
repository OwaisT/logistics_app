using LogisticsApp.Domain.BoundedContexts.Catalog.Aggregates.Order.ValueObjects;
using LogisticsApp.Domain.Common.Models;

namespace LogisticsApp.Domain.BoundedContexts.Catalog.Aggregates.Order;

public sealed class Order : AggregateRoot<OrderId, Guid>
{
    private List<OrderItem> _items = null!;
    public IReadOnlyList<OrderItem> Items => _items.AsReadOnly();
    private int TotalItems => _items.Sum(i => i.Quantity);
    public int TotalItemsCount { get; private set; }
    public decimal TotalValue { get; private set; }
    public string Status { get; private set; } = "Pending";

    private Order(
        OrderId id,
        List<OrderItem> items,
        decimal totalValue)
         : base(id)
    {
        _items = items;
        TotalItemsCount = TotalItems;
        TotalValue = totalValue;
    }

    internal static Order Create(
        List<OrderItem> items,
        decimal totalValue)
    {
        return new Order(
            OrderId.CreateUnique(),
            items,
            totalValue);
    }

#pragma warning disable CS8618
    private Order() : base(default!) { }
#pragma warning restore CS8618

}