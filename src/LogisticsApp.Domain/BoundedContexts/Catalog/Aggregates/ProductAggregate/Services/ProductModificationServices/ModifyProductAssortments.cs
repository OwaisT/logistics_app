using ErrorOr;
using LogisticsApp.Domain.BoundedContexts.Catalog.Aggregates.ProductAggregate.ValueObjects;
using LogisticsApp.Domain.Common.Errors;

namespace LogisticsApp.Domain.BoundedContexts.Catalog.Aggregates.ProductAggregate.Services.ProductModificationServices;

public static class ModifyProductAssortments
{
    // Create a public method to call for modifying product assortments.
    public static ErrorOr<Product> Execute(Product product, List<Assortment> assortments)
    {
        // Check if the color & size already exists in the product's colors list.
        foreach (var assortment in assortments)
        {
            // Check if the color & size already exist in the product's colors & sizes list.
            // If not, return an error.
            // If yes, modify the assortments list of the product.
            if (!product.Colors.Contains(assortment.Color))
            {
                return Errors.Common.PropertyNotFound("Product", nameof(assortment.Color));
            }
            foreach (var kv in assortment.Sizes)
            {
                if (!product.Sizes.Contains(kv.Key))
                {
                    return Errors.Common.PropertyNotFound("Product", nameof(kv.Key));
                }
            }
            product = product.ModifyAssortments(assortments);
        }

        return product;
    }
}