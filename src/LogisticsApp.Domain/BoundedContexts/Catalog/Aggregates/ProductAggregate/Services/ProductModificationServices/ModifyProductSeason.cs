using ErrorOr;
using LogisticsApp.Domain.Common.Errors;

namespace LogisticsApp.Domain.BoundedContexts.Catalog.Aggregates.ProductAggregate.Services.ProductModificationServices;

public class ModifyProductSeason(IProductUniquenessChecker _productUniquenessChecker)
{
    public ErrorOr<Product> ModifySeason(Product product, string newSeason)
    {
        if (string.IsNullOrWhiteSpace(newSeason))
        {
            return Errors.Common.CannotBeEmpty("Season");
        }

        if (product.Season == newSeason) return product;

        if (!_productUniquenessChecker.IsUnique(product.RefCode, newSeason))
        {
            return Errors.Product.DuplicateSeason(newSeason);
        }
        product = product.ModifySeason(newSeason);
        return product;
    }
}