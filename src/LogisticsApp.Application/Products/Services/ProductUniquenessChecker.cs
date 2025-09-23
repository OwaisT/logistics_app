using LogisticsApp.Application.Common.Interfaces.Persistence;
using LogisticsApp.Domain.BoundedContexts.Catalog.Aggregates.Product.Services;

namespace LogisticsApp.Application.Products.Services;

public class ProductUniquenessChecker(IProductRepository _productRepository) : IProductUniquenessChecker
{

    public bool IsUnique(string refCode, string season)
    {
        var existingProducts = _productRepository.GetAll();
        return !existingProducts.Any(p => p.RefCode == refCode && p.Season == season);
    }
}
