using LogisticsApp.Domain.Common.Exceptions;
using LogisticsApp.Domain.Common.Models;

namespace LogisticsApp.Domain.Aggregates.Order.ValueObjects;

public sealed class OrderId : AggregateRootId<Guid>
{
    public override Guid Value { get; protected set; }

    private OrderId(Guid value)
    {
        Value = value;
    }

    public static OrderId CreateUnique()
    {
        return new(Guid.NewGuid());
    }

    public static OrderId Create(Guid value)
    {
        if (value == Guid.Empty)
        {
            throw new CannotBeEmptyException(nameof(OrderId));
        }

        return new(value);
    }

    public override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }
}