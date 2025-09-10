using LogisticsApp.Domain.Common.Models;
using LogisticsApp.Domain.Common.Exceptions;

namespace LogisticsApp.Domain.Aggregates.Product.ValueObjects;

public sealed class ProductId : ValueObject
{
    private string Ref { get; }
    private string Season { get; }
    public string Value => $"{Ref}-{Season}";

    private ProductId(string refCode, string season)
    {
        Ref = refCode;
        Season = season;
    }

    public static ProductId Create(string refCode, string season)
    {
        if (string.IsNullOrWhiteSpace(refCode)) throw new CannotBeEmptyException(nameof(refCode));
        if (string.IsNullOrWhiteSpace(season)) throw new CannotBeEmptyException(nameof(season));
        return new ProductId(refCode, season);
    }

    public static ProductId CreateConversion(string value)
    {
        var parts = value.Split('-');
        if (parts.Length != 2) throw new InvalidFormatException(nameof(value), "Value must be in the format 'Ref-Season'");
        return Create(parts[0], parts[1]);
    }

    public override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }

    public override string ToString() => Value;
}
