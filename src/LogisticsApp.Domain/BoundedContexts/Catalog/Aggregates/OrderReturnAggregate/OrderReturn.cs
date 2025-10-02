using ErrorOr;
using LogisticsApp.Domain.BoundedContexts.Catalog.Aggregates.OrderAggregate;
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

    public ErrorOr<OrderReturn> UpdateStatus(string status)
    {
        // TODO: Add proper status validation
        var validStatuses = new List<string> { "Pending", "Approved", "Rejected", "Processed", "Received" };
        if (!validStatuses.Contains(status))
        {
            // TODO: create proper error
            return Error.Validation(
                code: "InvalidStatus",
                description: $"Status '{status}' is not valid. Valid statuses are: {string.Join(", ", validStatuses)}.");
        }
        Status = status;
        _items.ForEach(i => i.UpdateStatus(status));
        return this;
    }

#pragma warning disable CS8618
    private OrderReturn() : base(default!) { } // For EF Core
#pragma warning restore CS8618

}