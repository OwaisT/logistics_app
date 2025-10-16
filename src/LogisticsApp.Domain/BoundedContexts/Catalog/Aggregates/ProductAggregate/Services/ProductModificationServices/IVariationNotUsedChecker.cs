using LogisticsApp.Domain.BoundedContexts.Catalog.Aggregates.ProductAggregate.ValueObjects;

namespace LogisticsApp.Domain.BoundedContexts.Catalog.Aggregates.ProductAggregate.Services.ProductModificationServices;

public interface IVariationNotUsedChecker
{
    // TODO: Move the logic to application layer
    bool IsVariationUsed(ProductId productId, VariationId variationId);
}