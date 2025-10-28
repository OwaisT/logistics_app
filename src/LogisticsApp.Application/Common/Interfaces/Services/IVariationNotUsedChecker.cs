using LogisticsApp.Domain.BoundedContexts.Catalog.Aggregates.ProductAggregate.ValueObjects;

namespace LogisticsApp.Application.Common.Interfaces.Services;

public interface IVariationNotUsedChecker
{
    bool IsVariationUsed(ProductId productId, VariationId variationId);
}