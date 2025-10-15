using ErrorOr;
using LogisticsApp.Domain.Common.Errors;

namespace LogisticsApp.Domain.BoundedContexts.Catalog.Aggregates.ProductAggregate.Services.ProductModificationServices;

public class ModifyProductRefCode(IProductUniquenessChecker _productUniquenessChecker)
{
    public ErrorOr<Product> Execute(Product product, string newRefCode)
    {
        if (string.IsNullOrWhiteSpace(newRefCode))
        {
            return Errors.Common.CannotBeEmpty("RefCode");
        }

        if (product.RefCode == newRefCode)
            return product; // No change needed

        if (!_productUniquenessChecker.IsUnique(newRefCode, product.Season))
        {
            return Errors.Product.DuplicateRefCode(newRefCode, product.Season);
        }

        product = product.ModifyRefCode(newRefCode);
        return product;
    }
}