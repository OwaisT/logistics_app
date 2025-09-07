using LogisticsApp.Domain.Aggregates.Warehouse.Exceptions;
using LogisticsApp.Domain.Common.Models;

namespace LogisticsApp.Domain.Aggregates.Warehouse.ValueObjects;

public sealed class CartonId : ValueObject
{
    public Guid Value { get; }

    private CartonId(Guid value)
    {
        if (value == Guid.Empty)
        {
            throw new InvalidCartonException("Carton ID cannot be empty.");
        }

        Value = value;
    }

    public static CartonId CreateUnique()
    {
        return new CartonId(Guid.NewGuid());
    }

    public override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }
}
