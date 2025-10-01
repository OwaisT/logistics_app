using LogisticsApp.Domain.BoundedContexts.Catalog.Aggregates.Product.ValueObjects;

namespace LogisticsApp.Domain.BoundedContexts.Catalog.Services;

public interface IProductAvailabilityChecker
{
    bool IsProductVariationInStock(ProductId productId, VariationId variationId);
}
