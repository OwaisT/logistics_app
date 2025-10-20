using ErrorOr;
using LogisticsApp.Domain.Common.Errors;

namespace LogisticsApp.Domain.BoundedContexts.Catalog.Aggregates.ProductAggregate.Services.ProductModificationServices;

public static class ModifyProductRefCode
{
    public static ErrorOr<Product> Execute(Product product, string newRefCode, IProductUniquenessChecker productUniquenessChecker)
    {
        if (string.IsNullOrWhiteSpace(newRefCode))
        {
            return Errors.Common.CannotBeEmpty("RefCode");
        }

        if (product.RefCode == newRefCode)
            return product; // No change needed

        if (!productUniquenessChecker.IsUnique(newRefCode, product.Season))
        {
            return Errors.Product.DuplicateRefCode(newRefCode, product.Season);
        }

        product = product.ModifyRefCode(newRefCode);
        foreach (var item in product.Variations)
        {
            item.UpdateRefCode(newRefCode);
        }
        return product;
    }
}