using LogisticsApp.Domain.BoundedContexts.Catalog.Aggregates.Product.ValueObjects;
using LogisticsApp.Domain.Common.Models;

namespace LogisticsApp.Domain.BoundedContexts.Catalog.Aggregates.Order.ValueObjects;

public class OrderItem : ValueObject
{
    public ProductId ProductId { get; private set; }
    public VariationId VariationId { get; private set; }
    public string RefCode { get; private set; }
    public int Quantity { get; private set; }
    public string Status { get; private set; } = "Pending";

    private OrderItem(
        ProductId productId,
        VariationId variationId,
        string refCode,
        int quantity)
    {
        ProductId = productId;
        VariationId = variationId;
        RefCode = refCode;
        Quantity = quantity;
    }

    public static OrderItem Create(ProductId productId, VariationId variationId, string refCode, int quantity)
    {
        // Add any necessary validation here
        return new OrderItem(productId, variationId, refCode, quantity);
    }

    public void UpdateStatus(string status)
    {
        Status = status;
    }

    public override IEnumerable<object> GetEqualityComponents()
    {
        yield return ProductId;
        yield return VariationId;
        yield return Quantity;
    }
}