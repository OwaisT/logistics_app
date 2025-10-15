using ErrorOr;
using LogisticsApp.Domain.Common.Errors;

namespace LogisticsApp.Domain.BoundedContexts.Catalog.Aggregates.ProductAggregate.Services.ProductModificationServices;

public static class RemoveProductCategories
{
    public static ErrorOr<Product> Execute(Product product, List<string> categories)
    {
        if (categories == null || categories.Count == 0)
        {
            return Errors.Common.InvalidInput("categories");
        }

        var categoriesToRemove = product.Categories
            .Where(c => categories.Contains(c))
            .ToList();

        foreach (var category in categoriesToRemove)
        {
            if (!product.Categories.Contains(category))
            {
                return Errors.Common.PropertyNotFound("Product", category);
            }
            product = product.RemoveCategory(category);
        }
        return product;
    }
}