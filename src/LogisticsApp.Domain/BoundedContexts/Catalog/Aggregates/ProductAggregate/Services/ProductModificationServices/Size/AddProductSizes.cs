using ErrorOr;
using LogisticsApp.Domain.BoundedContexts.Catalog.Aggregates.ProductAggregate.Entities;
using LogisticsApp.Domain.BoundedContexts.Catalog.Aggregates.ProductAggregate.ValueObjects;
using LogisticsApp.Domain.Common.Errors;

namespace LogisticsApp.Domain.BoundedContexts.Catalog.Aggregates.ProductAggregate.Services.ProductModificationServices.Size;

public static class AddProductSizes
{

    public static ErrorOr<Product> Execute(Product product, List<string> sizes, List<Assortment> assortments)
    {
        foreach (var size in sizes)
        {
            var result = AddSize(product, size);
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

    private static ErrorOr<Product> AddSize(Product product, string size)
    {
        if (product.Sizes.Contains(size))
        {
            return Errors.Common.DuplicatePropertyValue("Product", "Size", size);
        }
        product = product.AddSize(size);
        var variations = GenerateVariationsForSize(product, size);
        product = product.AddVariations(variations);

        return product;
    }

    private static List<Variation> GenerateVariationsForSize(Product product, string size)
    {
        var variations = new List<Variation>();
        foreach (var color in product.Colors)
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
