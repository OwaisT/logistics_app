using LogisticsApp.Domain.BoundedContexts.Catalog.Aggregates.OrderAggregate.ValueObjects;
using LogisticsApp.Domain.BoundedContexts.Catalog.Aggregates.ProductAggregate.ValueObjects;
using LogisticsApp.Domain.Common.Models;

namespace LogisticsApp.Domain.BoundedContexts.Catalog.Aggregates.OrderAggregate.Entities;

public sealed class OrderItem : Entity<OrderItemId>
{
    public ProductId ProductId { get; private set; }
    public VariationId VariationId { get; private set; }
    public string RefCode { get; private set; }
    public string Status { get; private set; } = "Pending";

    private OrderItem(
        OrderItemId id,
        ProductId productId,
        VariationId variationId,
        string refCode)
        : base(id)
    {
        ProductId = productId;
        VariationId = variationId;
        RefCode = refCode;
    }

    public static OrderItem Create(ProductId productId, VariationId variationId, string refCode)
    {
        // Add any necessary validation here
        return new OrderItem(OrderItemId.CreateUnique(), productId, variationId, refCode);
    }

    internal void UpdateStatus(string status)
    {
        Status = status;
    }

}