using LogisticsApp.Domain.BoundedContexts.Catalog.Aggregates.QCItemAggregate;
using LogisticsApp.Domain.BoundedContexts.Catalog.Aggregates.QCItemAggregate.ValueObjects;

namespace LogisticsApp.Application.Common.Interfaces.Persistence;

public interface IQCItemRepository
{
    void Add(QCItem qcItem);

    QCItem? GetById(QCItemId id);

    void Update(QCItem qcItem);

}