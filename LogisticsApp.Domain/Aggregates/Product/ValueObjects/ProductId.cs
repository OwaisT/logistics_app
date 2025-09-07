using LogisticsApp.Domain.Common.Models;
using LogisticsApp.Domain.Exceptions;

namespace LogisticsApp.Domain.Aggregates.Product.ValueObjects;

public sealed class ProductId : ValueObject
{
    private string Ref { get; }
    private string Season { get; }
    public string ProductCode => $"{Ref}-{Season}";

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

    public override IEnumerable<object> GetEqualityComponents()
    {
        yield return ProductCode;
    }

    public override string ToString() => ProductCode;
}
