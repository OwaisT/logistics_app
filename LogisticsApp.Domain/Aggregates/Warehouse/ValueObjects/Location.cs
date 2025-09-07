using LogisticsApp.Domain.Common.Exceptions;
using LogisticsApp.Domain.Common.Models;

namespace LogisticsApp.Domain.Aggregates.Warehouse.ValueObjects;

public sealed class Location : ValueObject
{
    private int OnLeft { get; }
    private int Below { get; }
    private int Behind { get; }
    public string LocationString => $"{OnLeft}-{Below}-{Behind}";

    private Location(int onLeft, int below, int behind)
    {
        if (onLeft < 0)
        {
            throw new CannotBeEmptyException(nameof(onLeft));
        }

        if (below < 0)
        {
            throw new CannotBeEmptyException(nameof(below));
        }

        if (behind < 0)
        {
            throw new CannotBeEmptyException(nameof(behind));
        }

        OnLeft = onLeft;
        Below = below;
        Behind = behind;
    }

    public static Location Create(int onLeft, int below, int behind)
    {
        return new Location(onLeft, below, behind);
    }

    public override IEnumerable<object> GetEqualityComponents()
    {
        yield return LocationString;
    }

}