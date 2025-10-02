using LogisticsApp.Domain.BoundedContexts.Catalog.Aggregates.OrderAggregate.ValueObjects;
using LogisticsApp.Domain.BoundedContexts.Catalog.Aggregates.OrderReturnAggregate.Entities;
using LogisticsApp.Domain.BoundedContexts.Catalog.Aggregates.OrderReturnAggregate.ValueObjects;
using LogisticsApp.Domain.Common.Models;

namespace LogisticsApp.Domain.BoundedContexts.Catalog.Aggregates.OrderReturnAggregate;

public sealed class OrderReturn : AggregateRoot<OrderReturnId, Guid>
{
    private List<OrderReturnItem> _items = [];
    public OrderId OrderId { get; private set; }
    public IReadOnlyList<OrderReturnItem> Items => _items.AsReadOnly();
    public string Status { get; private set; } = "Pending";

    private OrderReturn(
        OrderReturnId id,
        OrderId orderId,
        List<OrderReturnItem> items,
        string status)
         : base(id)
    {
        OrderId = orderId;
        _items = items;
        Status = status;
    }

    internal static OrderReturn Create(
        OrderId orderId,
        List<OrderReturnItem> items,
        string status)
    {
        return new OrderReturn(
            OrderReturnId.CreateUnique(),
            orderId,
            items,
            status);
    }

#pragma warning disable CS8618
    private OrderReturn() : base(default!) { } // For EF Core
#pragma warning restore CS8618

}