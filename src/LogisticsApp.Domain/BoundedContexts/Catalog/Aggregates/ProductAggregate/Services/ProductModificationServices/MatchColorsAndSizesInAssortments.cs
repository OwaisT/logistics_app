using LogisticsApp.Domain.BoundedContexts.Catalog.Aggregates.ProductAggregate.ValueObjects;

namespace LogisticsApp.Domain.BoundedContexts.Catalog.Aggregates.ProductAggregate.Services.ProductModificationServices;

internal static class MatchColorsAndSizesInAssortments
{
    public static bool Execute(Product product, List<Assortment> assortments)
    {
        foreach (var assortment in assortments)
        {
            if (!product.Colors.Any(c => string.Equals(c, assortment.Color, StringComparison.OrdinalIgnoreCase)))
            {
            return false;
            }
            foreach (var size in assortment.Sizes)
            {
            if (!product.Sizes.Any(s => string.Equals(s, size.Key, StringComparison.OrdinalIgnoreCase)))
            {
                return false;
            }
            }
        }
        return true;
    }
}