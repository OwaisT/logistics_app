using LogisticsApp.Domain.Common.Models;

namespace LogisticsApp.Domain.Aggregates.Warehouse.ValueObjects;

public sealed class RoomId : ValueObject
{
    public Guid Value { get; }

    private RoomId(Guid value)
    {
        Value = value;
    }

    public static RoomId CreateUnique()
    {
        return new RoomId(Guid.NewGuid());
    }

    public static RoomId Create(Guid value)
    {
        return new RoomId(value);
    }

    public override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }
}
