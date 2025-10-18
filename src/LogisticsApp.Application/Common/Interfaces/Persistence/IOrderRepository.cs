using LogisticsApp.Domain.BoundedContexts.Catalog.Aggregates.OrderAggregate;
using LogisticsApp.Domain.BoundedContexts.Catalog.Aggregates.OrderAggregate.ValueObjects;
using LogisticsApp.Domain.BoundedContexts.Catalog.Aggregates.ProductAggregate.ValueObjects;

namespace LogisticsApp.Application.Common.Interfaces.Persistence;

public interface IOrderRepository
{
    void Add(Order order);

    Order? GetById(OrderId id);

    void Update(Order order);

    bool IsVariationUsed(ProductId productId, VariationId variationId);

    bool IsProductUsed(ProductId productId);
}