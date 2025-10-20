using ErrorOr;
using LogisticsApp.Domain.BoundedContexts.Catalog.Aggregates.ProductAggregate.ValueObjects;
using LogisticsApp.Domain.Common.Errors;

namespace LogisticsApp.Domain.BoundedContexts.Catalog.Aggregates.ProductAggregate.Services.ProductModificationServices.Color;

public static class RemoveProductColors
{
    public static ErrorOr<Product> Execute(Product product, List<string> colorsToRemove, List<VariationId>? variationsToRemove)
    {
        foreach (var color in colorsToRemove)
        {
            if (!product.Colors.Contains(color))
            {
                return Errors.Product.ColorNotFound(color);
            }
            product = product.RemoveColor(color);
            product = product.RemoveColorFromAssortments(color);
        }

        if (variationsToRemove is not null)
        {
            product = product.RemoveVariations(variationsToRemove!);
        }
        return product;
    }
    
}
