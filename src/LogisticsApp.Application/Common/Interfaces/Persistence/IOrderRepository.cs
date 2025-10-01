using LogisticsApp.Domain.BoundedContexts.Catalog.Aggregates.Order;
using LogisticsApp.Domain.BoundedContexts.Catalog.Aggregates.Order.ValueObjects;

namespace LogisticsApp.Application.Common.Interfaces.Persistence;

public interface IOrderRepository
{
    void Add(Order order);

    Order? GetById(OrderId id);

    void Update(Order order);

    
}