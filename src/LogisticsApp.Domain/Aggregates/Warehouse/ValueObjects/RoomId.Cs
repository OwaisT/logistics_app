using LogisticsApp.Domain.Common.Exceptions;
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
        if (value == Guid.Empty)
        {
            throw new CannotBeEmptyException(nameof(RoomId));
        }

        return new RoomId(value);
    }

    public override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }
}
