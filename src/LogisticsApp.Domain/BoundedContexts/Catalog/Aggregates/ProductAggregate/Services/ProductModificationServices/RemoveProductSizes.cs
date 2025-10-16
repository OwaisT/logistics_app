using ErrorOr;
using LogisticsApp.Domain.BoundedContexts.Catalog.Aggregates.ProductAggregate.ValueObjects;
using LogisticsApp.Domain.Common.Errors;

namespace LogisticsApp.Domain.BoundedContexts.Catalog.Aggregates.ProductAggregate.Services.ProductModificationServices;

public class RemoveProductSizes(IVariationNotUsedChecker _variationNotUsedChecker)
{
    public ErrorOr<Product> Execute(Product product, List<string> sizesToRemove)
    {
        foreach (var size in sizesToRemove)
        {
            if (!product.Sizes.Contains(size))
            {
                return Errors.Product.SizeNotFound(size);
            }
        }
        var notUsedResult = VariationsNotUsed(product, sizesToRemove);
        if (notUsedResult.IsError)
        {
            return notUsedResult.Errors;
        }

        foreach (var size in sizesToRemove)
        {
            product = product.RemoveSize(size);
        }
        product = product.RemoveVariations(notUsedResult.Value);
        return product;
    }

    private ErrorOr<List<VariationId>> VariationsNotUsed(Product product, List<string> sizesToRemove)
    {
        var variationsToCheck = product.Variations
            .Where(v => sizesToRemove.Contains(v.Size))
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
