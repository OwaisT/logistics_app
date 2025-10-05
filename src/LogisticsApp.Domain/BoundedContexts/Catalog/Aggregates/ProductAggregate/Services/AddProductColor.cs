using ErrorOr;
using LogisticsApp.Domain.BoundedContexts.Catalog.Aggregates.ProductAggregate.Entities;
using LogisticsApp.Domain.Common.Errors;

namespace LogisticsApp.Domain.BoundedContexts.Catalog.Aggregates.ProductAggregate.Services;

public class AddProductColor
{
    public static ErrorOr<Product> Execute(Product product, string color)
    {
        if (product.Colors.Contains(color))
        {
            return Errors.Common.DuplicatePropertyValue("Product", "Color", color);
        }
        product = product.AddColor(color);
        var variations = GenerateVariationsForColor(product, color);
        product = product.AddVariations(variations);

        return product;
    }

    private static List<Variation> GenerateVariationsForColor(Product product, string color)
    {
        var variations = new List<Variation>();
        foreach (var size in product.Sizes)
        {
            var variationName = $"{product.Name} - {color} - {size}";
            var variation = Variation.Create(
                product.RefCode,
                product.Season,
                variationName,
                product.Description,
                product.GeneralPrice,
                color,
                size);
            variations.Add(variation);
        }

        return variations;
    }
}
