using ErrorOr;
using LogisticsApp.Domain.Common.Errors;

namespace LogisticsApp.Domain.BoundedContexts.Catalog.Aggregates.ProductAggregate.Services.ProductModificationServices;

public static class ModifyProductSeason
{
    public static ErrorOr<Product> Execute(Product product, string newSeason, IProductUniquenessChecker productUniquenessChecker)
    {
        if (string.IsNullOrWhiteSpace(newSeason))
        {
            return Errors.Common.CannotBeEmpty("Season");
        }

        if (product.Season == newSeason) return product;

        if (!productUniquenessChecker.IsUnique(product.RefCode, newSeason))
        {
            return Errors.Product.DuplicateSeason(newSeason);
        }
        product = product.ModifySeason(newSeason);
        return product;
    }
}