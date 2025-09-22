using LogisticsApp.Domain.Common.Models;

namespace LogisticsApp.Domain.Aggregates.Warehouse.ValueObjects;

public sealed class WarehouseId : AggregateRootId<Guid>
{
    public override Guid Value { get; protected set; }

    private WarehouseId(Guid value)
    {
        Value = value;
    }

    public static WarehouseId CreateUnique()
    {
        return new(Guid.NewGuid());
    }

    public static WarehouseId Create(Guid value)
    {
        return new(value);
    }

    public override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }
}
