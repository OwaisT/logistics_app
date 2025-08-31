using LogisticsApp.Domain.Exceptions;
namespace LogisticsApp.Domain.ValueObjects;

public record VariationId(string Ref, string Color, string Season, string Size)
{
    public static VariationId Create(string refCode, string color, string season, string size)
    {
        if (string.IsNullOrWhiteSpace(refCode)) throw new CannotBeEmptyException(nameof(refCode));
        if (string.IsNullOrWhiteSpace(color)) throw new CannotBeEmptyException(nameof(color));
        if (string.IsNullOrWhiteSpace(season)) throw new CannotBeEmptyException(nameof(season));
        if (string.IsNullOrWhiteSpace(size)) throw new CannotBeEmptyException(nameof(size));
        return new VariationId(refCode, color, season, size);
    }
    
    public override string ToString() => $"{Ref}-{Color}-{Season}-{Size}";
}
