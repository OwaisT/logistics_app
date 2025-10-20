using ErrorOr;
using LogisticsApp.Domain.BoundedContexts.Catalog.Aggregates.ProductAggregate.Entities;
using LogisticsApp.Domain.Common.Errors;

namespace LogisticsApp.Domain.BoundedContexts.Catalog.Aggregates.ProductAggregate.Services.ProductModificationServices.Variations;

public static class AddProductVariations
{

    public static ErrorOr<Product> Execute(Product product, List<Dictionary<string, string>> colorSizeCombinations)
    {
        var variationsToAdd = new List<Variation>();
        foreach (var combination in colorSizeCombinations)
        {
            if (!combination.TryGetValue("color", out string? color) || !combination.TryGetValue("size", out string? size))
            {
                return Errors.Product.InvalidVariationCombinationFormat();
            }
            if (!product.Sizes.Contains(size))
            {
                return Errors.Common.PropertyNotFound("Product", nameof(size));
            }
            if (!product.Colors.Contains(color))
            {
                return Errors.Common.PropertyNotFound("Product", nameof(color));
            }
            if (product.Variations.Any(v => v.Color == color && v.Size == size))
            {
                return Errors.Product.DuplicateVariationCombination(color, size);
            }
            var variation = GenerateVariation(product, color, size);
            variationsToAdd.Add(variation);
        }

        product = product.AddVariations(variationsToAdd);
        return product;

    }

    private static Variation GenerateVariation(Product product, string color, string size)
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
        return variation;
    }

}