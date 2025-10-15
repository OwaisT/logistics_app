using ErrorOr;
using LogisticsApp.Domain.Common.Errors;

namespace LogisticsApp.Domain.BoundedContexts.Catalog.Aggregates.ProductAggregate.Services.ProductModificationServices;

public static class ModifyProductGeneralPrice
{
    public static ErrorOr<Product> Execute(Product product, decimal newPrice, bool updateVariationsPrices)
    {
        if (newPrice < 0)
        {
            return Errors.Common.CannotBeNegative("GeneralPrice");
        }

        product = product.ModifyGeneralPrice(newPrice);

        if (updateVariationsPrices)
        {
            foreach (var variation in product.Variations)
            {
                variation.UpdatePrice(newPrice);
            }
        }

        return product;
    }
}
