using ErrorOr;
using LogisticsApp.Domain.Common.Errors;

namespace LogisticsApp.Domain.BoundedContexts.Catalog.Aggregates.ProductAggregate.Services.ProductModificationServices.Variations;

public static class AddReceivedForVariationsService
{

    public static ErrorOr<Product> Execute(Product product, string color, int colorQuantity)
    {    
        var colorAssortment = product.Assortments.FirstOrDefault(a => string.Equals(a.Color, color, StringComparison.OrdinalIgnoreCase));
        if (colorAssortment == null)
        {
            return Errors.Product.ColorNotFound(color);
        }
        var assortmentTotal = colorAssortment.Sizes.Sum(s => s.Value);
        if (assortmentTotal == 0)
        {
            // TODO: Define a specific error for this case.
            return Error.Validation("AssortmentTotal", "Assortment total must be greater than zero.");
        }

        if (colorQuantity % assortmentTotal != 0)
        {
            // TODO: Define a specific error for this case.
            return Error.Validation("ColorQuantity", "Color quantity must be divisible by the assortment total.");
        }
        foreach (var size in colorAssortment.Sizes)
        {
            var variation = product.Variations.FirstOrDefault(
                v => string.Equals(v.Color, color, StringComparison.OrdinalIgnoreCase) 
                  && string.Equals(v.Size, size.Key, StringComparison.OrdinalIgnoreCase));
            if (variation == null)
            {
                return Errors.Common.EntityNotFound("Variation", $"{product.RefCode}-{color}-{size.Key}");
            }
                
            var sizeQuantity = (int)Math.Round(size.Value / (decimal)assortmentTotal * colorQuantity);
            variation.ModifyReceived(sizeQuantity);                
        }

        return product;
    }
}