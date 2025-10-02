using LogisticsApp.Domain.BoundedContexts.Catalog.Aggregates.ProductAggregate.ValueObjects;
using LogisticsApp.Domain.Common.Models;

namespace LogisticsApp.Domain.BoundedContexts.Positioning.Aggregates.Carton.ValueObjects;

public class CartonItem : ValueObject
{
    public ProductId ProductId { get; private set; }
    public VariationId VariationId { get; private set; }
    public string RefCode { get; private set; }
    public int Quantity { get; private set; }

    private CartonItem(ProductId productId, VariationId variationId, string refCode, int quantity)
    {
        ProductId = productId;
        VariationId = variationId;
        RefCode = refCode;
        Quantity = quantity;
    }

    public static CartonItem Create(ProductId productId, VariationId variationId, string refCode, int quantity)
    {
        // Add any necessary validation here
        return new CartonItem(productId, variationId, refCode, quantity);
    }

    public override IEnumerable<object> GetEqualityComponents()
    {
        yield return ProductId;
        yield return VariationId;
        yield return Quantity;
    }

}