using ErrorOr;
using LogisticsApp.Domain.BoundedContexts.Catalog.Aggregates.ProductAggregate.ValueObjects;
using LogisticsApp.Domain.Common.Errors;

namespace LogisticsApp.Domain.BoundedContexts.Catalog.Aggregates.ProductAggregate.Services.ProductModificationServices.Variations;

public static class RemoveProductVariations
{
    public static ErrorOr<Product> Execute(Product product, List<VariationId> variationIds)
    {
        product = product.RemoveVariations(variationIds);

        return product;
    }
    
}