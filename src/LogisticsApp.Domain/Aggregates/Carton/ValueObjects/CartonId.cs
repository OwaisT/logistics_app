using LogisticsApp.Domain.Common.Exceptions;
using LogisticsApp.Domain.Common.Models;

namespace LogisticsApp.Domain.Aggregates.Carton.ValueObjects;

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
        if (value == Guid.Empty)
        {
            throw new CannotBeEmptyException(nameof(CartonId));
        }

        return new(value);
    }

    public override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }

}
