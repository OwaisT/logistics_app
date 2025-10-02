using LogisticsApp.Domain.BoundedContexts.Catalog.Aggregates.ProductAggregate.ValueObjects;

namespace LogisticsApp.Domain.BoundedContexts.Catalog.Services;

public interface IProductAvailabilityChecker
{
    bool IsProductVariationInStock(ProductId productId, VariationId variationId);
}
