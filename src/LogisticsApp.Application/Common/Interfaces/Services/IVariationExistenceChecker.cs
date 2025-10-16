using ErrorOr;
using LogisticsApp.Domain.BoundedContexts.Catalog.Aggregates.ProductAggregate.ValueObjects;

namespace LogisticsApp.Application.Common.Interfaces.Services;

public interface IVariationExistenceChecker
{
    ErrorOr<string> ValidateVariationExistenceAndGetRefCode(ProductId productId, VariationId variationId);
}