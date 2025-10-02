using LogisticsApp.Application.Common.Interfaces.Persistence;
using LogisticsApp.Domain.BoundedContexts.Catalog.Aggregates.OrderReturnAggregate;

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

    public OrderReturn? GetById(Guid id)
    {
        return _dbContext.OrderReturns.Find(id);
    }

    public void Update(OrderReturn orderReturn)
    {
        _dbContext.OrderReturns.Update(orderReturn);
        _dbContext.SaveChanges();
    }
}