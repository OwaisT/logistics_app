using LogisticsApp.Domain.Exceptions;
namespace LogisticsApp.Domain.ValueObjects;

public record ProductId(string Ref, string Season)
{
    public static ProductId Create(string refCode, string season)
    {
        if (string.IsNullOrWhiteSpace(refCode)) throw new CannotBeEmptyException(nameof(refCode));
        if (string.IsNullOrWhiteSpace(season)) throw new CannotBeEmptyException(nameof(season));
        return new ProductId(refCode, season);
    }

    public override string ToString() => $"{Ref}-{Season}";
}
