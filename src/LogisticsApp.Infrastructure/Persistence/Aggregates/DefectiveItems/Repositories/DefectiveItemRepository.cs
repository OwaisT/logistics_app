using LogisticsApp.Application.Common.Interfaces.Persistence;
using LogisticsApp.Domain.BoundedContexts.Catalog.Aggregates.DefectiveItemAggregate;
using LogisticsApp.Domain.BoundedContexts.Catalog.Aggregates.DefectiveItemAggregate.ValueObjects;
using LogisticsApp.Domain.BoundedContexts.Catalog.Aggregates.ProductAggregate.ValueObjects;

namespace LogisticsApp.Infrastructure.Persistence.Aggregates.DefectiveItems.Repositories;

public class DefectiveItemRepository(LogisticsAppDbContext _dbContext) : IDefectiveItemRepository
{
    public void Add(DefectiveItem defectiveItem)
    {
        _dbContext.DefectiveItems.Add(defectiveItem);
        _dbContext.SaveChanges();
    }

    public DefectiveItem? GetById(DefectiveItemId id)
    {
        return _dbContext.DefectiveItems.Find(id);
    }

    public void Update(DefectiveItem defectiveItem)
    {
        _dbContext.DefectiveItems.Update(defectiveItem);
        _dbContext.SaveChanges();
    }

    public bool IsVariationUsed(ProductId productId, VariationId variationId)
    {
        return _dbContext.DefectiveItems.Any(di => di.ProductId == productId && di.VariationId == variationId);
    }

    public bool IsProductUsed(ProductId productId)
    {
        return _dbContext.DefectiveItems.Any(di => di.ProductId == productId);
    }
}