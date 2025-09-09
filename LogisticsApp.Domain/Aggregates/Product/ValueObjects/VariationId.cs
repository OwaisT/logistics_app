using LogisticsApp.Domain.Common.Models;
using LogisticsApp.Domain.Common.Exceptions;

namespace LogisticsApp.Domain.Aggregates.Product.ValueObjects;

public sealed class VariationId : ValueObject
{
    private ProductId ProductId { get; }
    private string Color { get; }
    private string Size { get; }
    public string VariationCode => $"{ProductId}-{Color}-{Size}";

    private VariationId(ProductId productId, string color, string size)
    {
        ProductId = productId;
        Color = color;
        Size = size;
    }

    public static VariationId Create(ProductId productId, string color, string size)
    {
        if (productId == null) throw new CannotBeEmptyException(nameof(productId));
        if (string.IsNullOrWhiteSpace(color)) throw new CannotBeEmptyException(nameof(color));
        if (string.IsNullOrWhiteSpace(size)) throw new CannotBeEmptyException(nameof(size));
        return new VariationId(productId, color, size);
    }

    public override IEnumerable<object> GetEqualityComponents()
    {
        yield return VariationCode;
    }

    public override string ToString() => VariationCode;
}
