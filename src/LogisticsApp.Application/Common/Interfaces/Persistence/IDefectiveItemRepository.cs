using LogisticsApp.Domain.BoundedContexts.Catalog.Aggregates.DefectiveItemAggregate;
using LogisticsApp.Domain.BoundedContexts.Catalog.Aggregates.DefectiveItemAggregate.ValueObjects;
using LogisticsApp.Domain.BoundedContexts.Catalog.Aggregates.ProductAggregate.ValueObjects;

namespace LogisticsApp.Application.Common.Interfaces.Persistence;

public interface IDefectiveItemRepository
{
    void Add(DefectiveItem defectiveItem);

    DefectiveItem? GetById(DefectiveItemId id);

    void Update(DefectiveItem defectiveItem);

    bool IsVariationUsed(ProductId productId, VariationId variationId);

    bool IsProductUsed(ProductId productId);
}