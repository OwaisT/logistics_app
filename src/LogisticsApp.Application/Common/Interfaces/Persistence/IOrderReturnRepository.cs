using LogisticsApp.Domain.BoundedContexts.Catalog.Aggregates.OrderReturnAggregate;
using LogisticsApp.Domain.BoundedContexts.Catalog.Aggregates.OrderReturnAggregate.ValueObjects;
using LogisticsApp.Domain.BoundedContexts.Catalog.Aggregates.ProductAggregate.ValueObjects;

namespace LogisticsApp.Application.Common.Interfaces.Persistence;

public interface IOrderReturnRepository
{
    void Add(OrderReturn orderReturn);
    OrderReturn? GetById(OrderReturnId id);
    void Update(OrderReturn orderReturn);

    bool IsVariationUsed(ProductId productId, VariationId variationId);

    bool IsProductUsed(ProductId productId);
}