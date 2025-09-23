using LogisticsApp.Domain.Common.Models;

namespace LogisticsApp.Domain.BoundedContexts.Catalog.Aggregates.OrderReturn.ValueObjects;

public sealed class OrderReturnId : AggregateRootId<Guid>
{
    public override Guid Value { get; protected set; }

    private OrderReturnId(Guid value)
    {
        Value = value;
    }

    public static OrderReturnId CreateUnique()
    {
        return new(Guid.NewGuid());
    }

    public static OrderReturnId Create(Guid value)
    {
        return new(value);
    }

    public override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }
}