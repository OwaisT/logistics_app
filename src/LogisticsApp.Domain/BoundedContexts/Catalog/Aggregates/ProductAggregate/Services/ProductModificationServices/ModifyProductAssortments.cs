using ErrorOr;
using LogisticsApp.Domain.BoundedContexts.Catalog.Aggregates.ProductAggregate.ValueObjects;
using LogisticsApp.Domain.Common.Errors;

namespace LogisticsApp.Domain.BoundedContexts.Catalog.Aggregates.ProductAggregate.Services.ProductModificationServices;

public static class ModifyProductAssortments
{
    // Create a public method to call for modifying product assortments.
    public static ErrorOr<Product> Execute(Product product, List<Assortment> assortments)
    {
        // Check if the color & size already exists in the product's colors list.
        // Ensure assortments cover all product colors and sizes before modifying.
        var assortmentColors = assortments.Select(a => a.Color).ToHashSet(StringComparer.OrdinalIgnoreCase);
        var assortmentSizes = assortments.SelectMany(a => a.Sizes.Keys).ToHashSet(StringComparer.OrdinalIgnoreCase);

        if (!product.Colors.All(color => assortmentColors.Contains(color, StringComparer.OrdinalIgnoreCase)))
        {
            return Errors.Common.PropertyNotFound("Product", nameof(Product.Colors));
        }

        if (!product.Sizes.All(size => assortmentSizes.Contains(size, StringComparer.OrdinalIgnoreCase)))
        {
            return Errors.Common.PropertyNotFound("Product", nameof(Product.Sizes));
        }

        product = product.ModifyAssortments(assortments);

        return product;
    }
}