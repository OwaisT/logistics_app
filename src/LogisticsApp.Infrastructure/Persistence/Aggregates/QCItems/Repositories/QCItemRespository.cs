using LogisticsApp.Application.Common.Interfaces.Persistence;
using LogisticsApp.Domain.BoundedContexts.Catalog.Aggregates.QCItemAggregate;
using LogisticsApp.Domain.BoundedContexts.Catalog.Aggregates.QCItemAggregate.ValueObjects;

namespace LogisticsApp.Infrastructure.Persistence.Aggregates.QCItems.Repositories;

public class QCItemRepository(LogisticsAppDbContext _dbContext) : IQCItemRepository
{
    public void Add(QCItem qcItem)
    {
        _dbContext.Add(qcItem);
        _dbContext.SaveChanges();
    }

    public QCItem? GetById(QCItemId id)
    {
        return _dbContext.Find<QCItem>(id);
    }

    public void Update(QCItem qcItem)
    {
        _dbContext.Update(qcItem);
        _dbContext.SaveChanges();
    }
}