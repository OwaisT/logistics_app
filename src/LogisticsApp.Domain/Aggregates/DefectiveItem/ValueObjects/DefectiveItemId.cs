using LogisticsApp.Domain.Common.Models;

namespace LogisticsApp.Domain.Aggregates.DefectiveItem.ValueObjects;

public sealed class DefectiveItemId : AggregateRootId<Guid>
{
    public override Guid Value { get; protected set; }

    private DefectiveItemId(Guid value)
    {
        Value = value;
    }

    public static DefectiveItemId CreateUnique()
    {
        return new(Guid.NewGuid());
    }

    public static DefectiveItemId Create(Guid value)
    {
        return new(value);
    }

    public override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }
}