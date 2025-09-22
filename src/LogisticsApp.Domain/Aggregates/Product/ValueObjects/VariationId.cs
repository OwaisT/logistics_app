using LogisticsApp.Domain.Common.Models;

namespace LogisticsApp.Domain.Aggregates.Product.ValueObjects;

public sealed class VariationId : ValueObject
{
    public Guid Value { get; }

    private VariationId(Guid value)
    {
        Value = value;
    }

    public static VariationId CreateUnique()
    {
        return new VariationId(Guid.NewGuid());
    }

    public static VariationId Create(Guid value)
    {
        return new VariationId(value);
    }

    public override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }
}
