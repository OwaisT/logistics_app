using ErrorOr;
using LogisticsApp.Domain.BoundedContexts.Catalog.Aggregates.ProductAggregate.ValueObjects;
using LogisticsApp.Domain.Common.Errors;

namespace LogisticsApp.Domain.BoundedContexts.Catalog.Aggregates.ProductAggregate.Services.ProductModificationServices.Size;

public static class RemoveProductSizes
{
    public static ErrorOr<Product> Execute(Product product, List<string> sizesToRemove, List<VariationId>? variationsToRemove)
    {
        foreach (var size in sizesToRemove)
        {
            if (!product.Sizes.Contains(size))
            {
                return Errors.Product.SizeNotFound(size);
            }
            product = product.RemoveSize(size);
        }
        if (variationsToRemove is not null)
        {
            product = product.RemoveVariations(variationsToRemove);
        }
        return product;
    }
}
