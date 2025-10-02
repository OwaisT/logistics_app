using LogisticsApp.Domain.BoundedContexts.Catalog.Aggregates.OrderAggregate.ValueObjects;
using LogisticsApp.Domain.BoundedContexts.Catalog.Aggregates.OrderReturnAggregate.ValueObjects;
using LogisticsApp.Domain.BoundedContexts.Catalog.Aggregates.ProductAggregate.ValueObjects;
using LogisticsApp.Domain.Common.Models;

namespace LogisticsApp.Domain.BoundedContexts.Catalog.Aggregates.OrderReturnAggregate.Entities;

public sealed class OrderReturnItem : Entity<OrderReturnItemId>
{
    public OrderItemId OrderItemId { get; private set; }
    public ProductId ProductId { get; private set; }
    public VariationId VariationId { get; private set; }
    public string RefCode { get; private set; }
    public string Status { get; private set; } = "Pending";

    private OrderReturnItem(
        OrderReturnItemId id,
        OrderItemId orderItemId,
        ProductId productId,
        VariationId variationId,
        string refCode)
        : base(id)
    {
        OrderItemId = orderItemId;
        ProductId = productId;
        VariationId = variationId;
        RefCode = refCode;
    }

    public static OrderReturnItem Create(OrderItemId orderItemId, ProductId productId, VariationId variationId, string refCode)
    {
        // Add any necessary validation here
        return new OrderReturnItem(OrderReturnItemId.CreateUnique(), orderItemId, productId, variationId, refCode);
    }

    public void UpdateStatus(string status)
    {
        Status = status;
    }
}
