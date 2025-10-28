using LogisticsApp.Domain.BoundedContexts.Catalog.Aggregates.ProductAggregate.ValueObjects;

namespace LogisticsApp.Application.Common.Interfaces.Services;

public interface IProductNotUsedChecker
{
    bool IsProductUsed(ProductId productId);
}
