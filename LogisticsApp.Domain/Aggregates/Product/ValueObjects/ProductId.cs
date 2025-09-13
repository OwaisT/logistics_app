using LogisticsApp.Domain.Common.Models;
using LogisticsApp.Domain.Common.Exceptions;

namespace LogisticsApp.Domain.Aggregates.Product.ValueObjects;

public sealed class ProductId : AggregateRootId<Guid>
{
    public override Guid Value { get; protected set; }

    private ProductId(Guid value)
    {
        Value = value;
    }

    public static ProductId CreateUnique()
    {
        return new(Guid.NewGuid());
    }

    public static ProductId Create(Guid value)
    {
        if (value == Guid.Empty)
        {
            throw new CannotBeEmptyException(nameof(ProductId));
        }

        return new(value);
    }

    public override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }

}
