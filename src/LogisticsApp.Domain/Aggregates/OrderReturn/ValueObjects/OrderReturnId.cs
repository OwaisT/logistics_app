using LogisticsApp.Domain.Common.Exceptions;
using LogisticsApp.Domain.Common.Models;

namespace LogisticsApp.Domain.Aggregates.OrderReturn.ValueObjects;

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
        if (value == Guid.Empty)
        {
            throw new CannotBeEmptyException(nameof(OrderReturnId));
        }

        return new(value);
    }

    public override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }
}