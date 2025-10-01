using LogisticsApp.Application.Common.Interfaces.Persistence;
using LogisticsApp.Domain.BoundedContexts.Catalog.Aggregates.Product.Services;

namespace LogisticsApp.Application.Aggregates.Products.Services;

public class ProductUniquenessChecker(IProductRepository _productRepository) : IProductUniquenessChecker
{

    public bool IsUnique(string refCode, string season)
    {
        var existingProduct = _productRepository.GetByDetails(refCode, season);
        return existingProduct == null;
    }
}
