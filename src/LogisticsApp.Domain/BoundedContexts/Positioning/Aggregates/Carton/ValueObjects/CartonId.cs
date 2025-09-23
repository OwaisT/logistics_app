using LogisticsApp.Domain.Common.Models;

namespace LogisticsApp.Domain.BoundedContexts.Positioning.Aggregates.Carton.ValueObjects;

public sealed class CartonId : AggregateRootId<Guid>
{
    public override Guid Value { get; protected set; }

    private CartonId(Guid value)
    {
        Value = value;
    }

    public static CartonId CreateUnique()
    {
        return new(Guid.NewGuid());
    }

    public static CartonId Create(Guid value)
    {
        return new(value);
    }

    public override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }

}
