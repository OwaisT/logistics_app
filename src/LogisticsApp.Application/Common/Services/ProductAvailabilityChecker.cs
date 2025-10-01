using LogisticsApp.Application.Common.Interfaces.Persistence;
using LogisticsApp.Domain.BoundedContexts.Catalog.Aggregates.Product.ValueObjects;
using LogisticsApp.Domain.BoundedContexts.Catalog.Services;

namespace LogisticsApp.Application.Common.Services;

public class ProductAvailabilityChecker(
    IProductRepository _productRepository)
     : IProductAvailabilityChecker
{
    public bool IsProductVariationInStock(ProductId productId, VariationId variationId)
    {
        var product = _productRepository.GetById(productId);
        if (product == null)
        {
            return false; // Product does not exist
        }
        var variation = product.Variations.FirstOrDefault(v => v.Id == variationId);
        if (variation == null)
        {
            return false; // Variation does not exist
        }
        if (variation.Available < 1)
        {
            return false; // Not enough stock
        }
        return true;
    }
}