using ErrorOr;
using LogisticsApp.Domain.BoundedContexts.Catalog.Aggregates.ProductAggregate.ValueObjects;
using LogisticsApp.Domain.Common.Errors;

namespace LogisticsApp.Domain.BoundedContexts.Catalog.Aggregates.ProductAggregate.Services.ProductModificationServices;

public class RemoveProductVariations(IVariationNotUsedChecker _variationNotUsedChecker)
{
    public ErrorOr<Product> RemoveVariations(Product product, List<VariationId> variationIds)
    {
        foreach (var variationId in variationIds)
        {
            if (!product.Variations.Any(v => v.Id == variationId))
            {
                return Errors.Common.EntityNotFound("Variation", variationId.Value.ToString());
            }

            if (_variationNotUsedChecker.IsVariationUsed(product.Id, variationId))
            {
                var variationInUse = product.Variations.FirstOrDefault(v => v.Id == variationId);
                return Errors.Product.VariationInUse(variationInUse!.Color, variationInUse.Size);
            }

        }
        product = product.RemoveVariations(variationIds);

        return product;
    }
    
}