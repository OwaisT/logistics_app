using ErrorOr;
using LogisticsApp.Application.Common.Interfaces.Persistence;
using LogisticsApp.Application.Common.Interfaces.Services;
using LogisticsApp.Domain.BoundedContexts.Catalog.Aggregates.ProductAggregate.ValueObjects;
using LogisticsApp.Domain.Common.Errors;

namespace LogisticsApp.Application.Aggregates.Orders.Services;

public class ProductAvailabilityChecker(
    IProductRepository _productRepository)
     : IProductAvailabilityChecker
{
    public ErrorOr<string> ValidateProductAvailabilityAndGetVariationRefCode(ProductId productId, VariationId variationId, int quantity)
    {
        var product = _productRepository.GetById(productId);
        if (product == null)
        {
            return Errors.Common.EntityNotFound(nameof(product), productId.Value.ToString()); // Product does not exist
        }
        var variation = product.Variations.FirstOrDefault(v => v.Id == variationId);
        if (variation == null)
        {
            return Errors.Common.EntityNotFound(nameof(variation), variationId.Value.ToString()); // Variation does not exist
        }
        if (variation.Available < quantity)
        {
            return Errors.Product.InsufficientStock(variationId.Value.ToString()); // Not enough stock
        }
        return variation.VariationRefCode;
    }
}