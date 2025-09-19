using LogisticsApp.Domain.Aggregates.Product.ValueObjects;
using LogisticsApp.Domain.Common.Models;

namespace LogisticsApp.Domain.Aggregates.OrderReturn.ValueObjects;

public class OrderReturnItem : ValueObject
{
    public ProductId ProductId { get; }
    public VariationId VariationId { get; }
    public string RefCode { get; }
    public int Quantity { get; }

    public OrderReturnItem(ProductId productId, VariationId variationId, string refCode, int quantity)
    {
        ProductId = productId;
        VariationId = variationId;
        RefCode = refCode;
        Quantity = quantity;
    }

    public override IEnumerable<object> GetEqualityComponents()
    {
        yield return ProductId;
        yield return VariationId;
        yield return Quantity;
    }
}