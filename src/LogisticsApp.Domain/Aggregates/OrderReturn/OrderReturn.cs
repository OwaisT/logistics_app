using LogisticsApp.Domain.Aggregates.Order.ValueObjects;
using LogisticsApp.Domain.Aggregates.OrderReturn.ValueObjects;
using LogisticsApp.Domain.Common.Models;

namespace LogisticsApp.Domain.Aggregates.OrderReturn;

public sealed class OrderReturn : AggregateRoot<OrderReturnId, Guid>
{
    private List<OrderReturnItem> _items = [];
    public OrderId OrderId { get; private set; }
    public IReadOnlyList<OrderReturnItem> Items => _items.AsReadOnly();
    public int TotalItems => _items.Sum(i => i.Quantity);
    public decimal TotalPrice { get; private set; }
    public string Status { get; private set; } = "Pending";

    private OrderReturn(
        OrderReturnId id,
        OrderId orderId,
        List<OrderReturnItem> items,
        decimal totalPrice,
        string status)
         : base(id)
    {
        OrderId = orderId;
        _items = items;
        TotalPrice = totalPrice;
        Status = status;
    }

    public static OrderReturn Create(
        OrderId orderId,
        List<OrderReturnItem> items,
        decimal totalPrice,
        string status)
    {
        return new OrderReturn(
            OrderReturnId.CreateUnique(),
            orderId,
            items,
            totalPrice,
            status);
    }

}