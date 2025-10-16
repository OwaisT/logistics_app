using ErrorOr;
using LogisticsApp.Domain.BoundedContexts.Catalog.Aggregates.ProductAggregate.ValueObjects;

namespace LogisticsApp.Application.Common.Interfaces.Services;

public interface IProductAvailabilityChecker
{
    ErrorOr<string> ValidateProductAvailabilityAndGetVariationRefCode(ProductId productId, VariationId variationId, int quantity);
}