using ErrorOr;
using LogisticsApp.Application.Common.Interfaces.Persistence;
using LogisticsApp.Application.Common.Interfaces.Services;
using LogisticsApp.Domain.BoundedContexts.Catalog.Aggregates.ProductAggregate.ValueObjects;
using LogisticsApp.Domain.Common.Errors;

namespace LogisticsApp.Application.Aggregates.Cartons.Services;

public class VariationExistenceChecker(IProductRepository _productRepository) : IVariationExistenceChecker
{
    public ErrorOr<string> ValidateVariationExistenceAndGetRefCode(ProductId productId, VariationId variationId)
    {
        var product = _productRepository.GetById(productId);
        if (product is null)
        {
            return Errors.Common.EntityNotFound(nameof(product), productId.Value.ToString());
        }
        var variation = product.Variations.FirstOrDefault(v => v.Id == variationId);
        if (variation is null)
        {
            return Errors.Common.EntityNotFound(nameof(variation), variationId.Value.ToString());
        }
        return variation.VariationRefCode;
    }
}