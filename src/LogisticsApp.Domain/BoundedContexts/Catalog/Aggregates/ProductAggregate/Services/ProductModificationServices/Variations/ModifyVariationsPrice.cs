using ErrorOr;
using LogisticsApp.Domain.Common.Errors;

namespace LogisticsApp.Domain.BoundedContexts.Catalog.Aggregates.ProductAggregate.Services.ProductModificationServices.Variations;

public static class ModifyVariationsPrice
{
    public static ErrorOr<Product> Execute(Product product, decimal newPrice, string color)
    {
        if (newPrice < 0)
        {
            return Errors.Common.CannotBeNegativeOrZero("newPrice");
        }
        var variationsToModify = product.Variations
            .Where(v => v.Color.Equals(color, StringComparison.OrdinalIgnoreCase))
            .ToList();

        foreach (var variation in variationsToModify)
        {
            variation.UpdatePrice(newPrice);
        }

        return product;
    }
}