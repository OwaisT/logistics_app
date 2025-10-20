using ErrorOr;
using LogisticsApp.Domain.BoundedContexts.Catalog.Aggregates.ProductAggregate.Entities;
using LogisticsApp.Domain.BoundedContexts.Catalog.Aggregates.ProductAggregate.ValueObjects;
using LogisticsApp.Domain.Common.Errors;

namespace LogisticsApp.Domain.BoundedContexts.Catalog.Aggregates.ProductAggregate.Services.ProductModificationServices.Color;

public static class AddProductColors
{

    public static ErrorOr<Product> Execute(Product product, List<string> colors, List<Assortment> assortments)
    {
        foreach (var color in colors)
        {
            var result = AddColor(product, color);
            if (result.IsError)
            {
                return result;
            }
            product = result.Value;
        }
        if (!MatchColorsAndSizesInAssortments.Execute(product, assortments))
        {
            return Errors.Product.InvalidAssortmentColorsOrSizes();
        }
        product = product.ModifyAssortments(assortments);

        return product;
    }

    private static ErrorOr<Product> AddColor(Product product, string color)
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
            if (product.Variations.Any(v => v.Color == color && v.Size == size))
            {
                continue; // Variation already exists
            }
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
