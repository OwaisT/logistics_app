using ErrorOr;
using LogisticsApp.Application.Common.Interfaces.Persistence;
using LogisticsApp.Domain.BoundedContexts.Catalog.Aggregates.ProductAggregate;
using LogisticsApp.Domain.BoundedContexts.Catalog.Aggregates.ProductAggregate.ValueObjects;
using LogisticsApp.Domain.Common.Errors;

namespace LogisticsApp.Application.Common.Services;

public class EnforceProductInvariantsAndGetVariationRefCodeService(
    IProductRepository _productRepository)
{
    public ErrorOr<string> Enforce(ProductId productId, VariationId variationId)
    {
        if (productId == null || variationId == null)
        {
            return Errors.Common.InvalidInput("Invalid product or variation information.");
        }
        var product = _productRepository.GetById(productId);
        if (product is null)
        {
            return Errors.Common.EntityNotFound(nameof(Product), productId.Value.ToString());
        }
        var variation = product.GetVariation(variationId);
        if (variation is null)
        {
            return Errors.Common.EntityNotFound(nameof(Product) + " Variation", variationId.Value.ToString());
        }
        return variation.VariationRefCode;
    }
}