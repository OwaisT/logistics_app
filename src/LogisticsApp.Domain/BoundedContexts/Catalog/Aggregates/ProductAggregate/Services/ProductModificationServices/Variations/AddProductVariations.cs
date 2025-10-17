using ErrorOr;
using LogisticsApp.Domain.BoundedContexts.Catalog.Aggregates.ProductAggregate.Entities;
using LogisticsApp.Domain.Common.Errors;

namespace LogisticsApp.Domain.BoundedContexts.Catalog.Aggregates.ProductAggregate.Services.ProductModificationServices.Variations;

public static class AddProductVariations
{

    public static ErrorOr<Product> Execute(Product product, List<string> colors, List<string> sizes)
    {
        foreach (var size in sizes)
        {
            if (!product.Sizes.Contains(size))
            {
                return Errors.Common.PropertyNotFound("Product", nameof(size));
            }
        }
        foreach (var color in colors)
        {
            if (!product.Colors.Contains(color))
            {
                return Errors.Common.PropertyNotFound("Product", nameof(color));
            }
        }

        // TODO: Check for existing variations to avoid duplicates
        var variations = GenerateVariations(product, colors, sizes);
        product = product.AddVariations(variations);
        return product;

    }

    private static List<Variation> GenerateVariations(Product product, List<string> colors, List<string> sizes)
    {
        var variations = new List<Variation>();
        foreach (var color in colors)
        {
            foreach (var size in sizes)
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
        }

        return variations;
    }

}