using LogisticsApp.Application.Common.Interfaces.Persistence;
using LogisticsApp.Domain.BoundedContexts.Catalog.Aggregates.OrderReturnAggregate;
using LogisticsApp.Domain.BoundedContexts.Catalog.Aggregates.OrderReturnAggregate.ValueObjects;
using LogisticsApp.Domain.BoundedContexts.Catalog.Aggregates.ProductAggregate.ValueObjects;

namespace LogisticsApp.Infrastructure.Persistence.Aggregates.OrderReturns.Repositories;

public class OrderReturnRepository(
    LogisticsAppDbContext _dbContext)
     : IOrderReturnRepository
{
    public void Add(OrderReturn orderReturn)
    {
        _dbContext.OrderReturns.Add(orderReturn);
        _dbContext.SaveChanges();
    }

    public OrderReturn? GetById(OrderReturnId id)
    {
        return _dbContext.OrderReturns.Find(id);
    }

    public void Update(OrderReturn orderReturn)
    {
        _dbContext.OrderReturns.Update(orderReturn);
        _dbContext.SaveChanges();
    }

    public bool IsVariationUsed(ProductId productId, VariationId variationId)
    {
        return _dbContext.OrderReturns.Any(or => or.Items.Any(i => i.ProductId == productId && i.VariationId == variationId));
    }

    public bool IsProductUsed(ProductId productId)
    {
        return _dbContext.OrderReturns.Any(or => or.Items.Any(i => i.ProductId == productId));
    }
}