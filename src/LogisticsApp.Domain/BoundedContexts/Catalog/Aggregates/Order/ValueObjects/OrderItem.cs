using LogisticsApp.Domain.BoundedContexts.Catalog.Aggregates.Product.ValueObjects;
using LogisticsApp.Domain.Common.Models;

namespace LogisticsApp.Domain.BoundedContexts.Catalog.Aggregates.Order.ValueObjects;

public class OrderItem(ProductId productId, VariationId variationId, string refCode, int quantity) : ValueObject
{
    public ProductId ProductId { get; } = productId;
    public VariationId VariationId { get; } = variationId;
    public string RefCode { get; } = refCode;
    public int Quantity { get; } = quantity;
    public string Status { get; private set; } = "Pending";

    public override IEnumerable<object> GetEqualityComponents()
    {
        yield return ProductId;
        yield return VariationId;
        yield return Quantity;
    }
}