using LogisticsApp.Application.Common.Interfaces.Persistence;
using LogisticsApp.Domain.BoundedContexts.Catalog.Aggregates.OrderAggregate;
using LogisticsApp.Domain.BoundedContexts.Catalog.Aggregates.OrderAggregate.ValueObjects;
using LogisticsApp.Domain.BoundedContexts.Catalog.Aggregates.ProductAggregate.ValueObjects;

namespace LogisticsApp.Infrastructure.Persistence.Aggregates.Orders.Repositories;

public class OrderRepository(LogisticsAppDbContext _dbContext) : IOrderRepository
{
    public void Add(Order order)
    {
        _dbContext.Add(order);
        _dbContext.SaveChanges();
    }

    public Order? GetById(OrderId id)
    {
        return _dbContext.Find<Order>(id);
    }

    public void Update(Order order)
    {
        _dbContext.Update(order);
        _dbContext.SaveChanges();
    }

    public bool IsVariationUsed(ProductId productId, VariationId variationId)
    {
        return _dbContext.Orders.Any(o => o.Items.Any(i => i.ProductId == productId && i.VariationId == variationId));
    }

    public bool IsProductUsed(ProductId productId)
    {
        return _dbContext.Orders.Any(o => o.Items.Any(i => i.ProductId == productId));
    }
}