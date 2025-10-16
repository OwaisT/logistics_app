using ErrorOr;
using LogisticsApp.Domain.BoundedContexts.Catalog.Aggregates.OrderAggregate.Entities;
using LogisticsApp.Domain.BoundedContexts.Catalog.Aggregates.OrderAggregate.ValueObjects;
using LogisticsApp.Domain.Common.Models;

namespace LogisticsApp.Domain.BoundedContexts.Catalog.Aggregates.OrderAggregate;

public sealed class Order : AggregateRoot<OrderId, Guid>
{
    private List<OrderItem> _items = null!;
    public IReadOnlyList<OrderItem> Items => _items.AsReadOnly();
    private int TotalItems => _items.Count;
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

    public static Order Create(
        List<OrderItem> items,
        decimal totalValue)
    {
        return new Order(
            OrderId.CreateUnique(),
            items,
            totalValue);
    }

    internal ErrorOr<Order> UpdateStatus(string status)
    {
        Status = status;
        _items.ForEach(i => i.UpdateStatus(status));
        return this;
    }

    internal Order UpdateItemsStatus(List<OrderItem> items, string status)
    {
        foreach (var item in items)
        {
            var orderItem = _items.FirstOrDefault(i => i.Id == item.Id);
            orderItem?.UpdateStatus(status);
        }
        return this;
    }

#pragma warning disable CS8618
    private Order() : base(default!) { }
#pragma warning restore CS8618

}