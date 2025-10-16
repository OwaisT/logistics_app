using ErrorOr;
using LogisticsApp.Domain.BoundedContexts.Catalog.Aggregates.ProductAggregate.ValueObjects;
using LogisticsApp.Domain.Common.Errors;

namespace LogisticsApp.Domain.BoundedContexts.Catalog.Aggregates.ProductAggregate.Services.ProductModificationServices;

public class RemoveProductColors(IVariationNotUsedChecker _variationNotUsedChecker)
{
    public ErrorOr<Product> Execute(Product product, List<string> colorsToRemove)
    {
        foreach (var color in colorsToRemove)
        {
            if (!product.Colors.Contains(color))
            {
                return Errors.Product.ColorNotFound(color);
            }
        }
        var notUsedResult = VariationsNotUsed(product, colorsToRemove);
        if (notUsedResult.IsError)
        {
            return notUsedResult.Errors;
        }

        foreach (var color in colorsToRemove)
        {
            product = product.RemoveColor(color);
        }
        product = product.RemoveVariations(notUsedResult.Value);
        return product;
    }
    
    private ErrorOr<List<VariationId>> VariationsNotUsed(Product product, List<string> colorsToRemove)
    {
        var variationsToCheck = product.Variations
            .Where(v => colorsToRemove.Contains(v.Color))
            .ToList();
        // TODO: Move the logic to application layer
        foreach (var variation in variationsToCheck)
        {
            if (_variationNotUsedChecker.IsVariationUsed(product.Id, variation.Id))
            {
                return Errors.Product.VariationInUse(variation.Color, variation.Size);
            }
        }
        var variationsToRemoveIds = variationsToCheck.Select(v => v.Id).ToList();
        return variationsToRemoveIds;
    }
    
}
