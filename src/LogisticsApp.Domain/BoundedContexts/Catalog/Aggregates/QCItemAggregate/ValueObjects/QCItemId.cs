using LogisticsApp.Domain.Common.Models;

namespace LogisticsApp.Domain.BoundedContexts.Catalog.Aggregates.QCItemAggregate.ValueObjects;

public sealed class QCItemId : AggregateRootId<Guid>
{
    public override Guid Value { get; protected set; }

    private QCItemId(Guid value)
    {
        Value = value;
    }

    public static QCItemId CreateUnique()
    {
        return new(Guid.NewGuid());
    }

    public static QCItemId Create(Guid value)
    {
        return new(value);
    }

    public override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }


}
