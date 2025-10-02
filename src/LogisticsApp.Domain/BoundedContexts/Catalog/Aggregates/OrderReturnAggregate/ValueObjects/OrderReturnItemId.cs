using LogisticsApp.Domain.Common.Models;

namespace LogisticsApp.Domain.BoundedContexts.Catalog.Aggregates.OrderReturnAggregate.ValueObjects;

public sealed class OrderReturnItemId : ValueObject
{
    public Guid Value { get; }

    private OrderReturnItemId(Guid value)
    {
        Value = value;
    }

    public static OrderReturnItemId CreateUnique()
    {
        return new OrderReturnItemId(Guid.NewGuid());
    }

    public static OrderReturnItemId Create(Guid value)
    {
        return new OrderReturnItemId(value);
    }

    public override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }
}
