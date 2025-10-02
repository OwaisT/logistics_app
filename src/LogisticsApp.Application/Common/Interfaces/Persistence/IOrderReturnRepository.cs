using LogisticsApp.Domain.BoundedContexts.Catalog.Aggregates.OrderReturnAggregate;

namespace LogisticsApp.Application.Common.Interfaces.Persistence;

public interface IOrderReturnRepository
{
    void Add(OrderReturn orderReturn);
    OrderReturn? GetById(Guid id);
    void Update(OrderReturn orderReturn);
}