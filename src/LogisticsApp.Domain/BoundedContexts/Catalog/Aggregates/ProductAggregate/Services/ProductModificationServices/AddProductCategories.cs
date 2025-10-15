using ErrorOr;
using LogisticsApp.Domain.Common.Errors;

namespace LogisticsApp.Domain.BoundedContexts.Catalog.Aggregates.ProductAggregate.Services.ProductModificationServices;

public static class AddProductCategories
{
    public static ErrorOr<Product> Execute(Product product, List<string> categories)
    {
        foreach (var category in categories)
        {
            if (product.Categories.Contains(category))
            {
                return Errors.Common.DuplicatePropertyValue("Product", "Category", category);
            }
            product = product.AddCategory(category);
        }
        return product;
    }
}