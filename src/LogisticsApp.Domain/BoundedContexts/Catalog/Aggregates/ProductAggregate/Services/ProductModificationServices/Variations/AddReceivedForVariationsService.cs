using ErrorOr;
using LogisticsApp.Domain.Common.Errors;

namespace LogisticsApp.Domain.BoundedContexts.Catalog.Aggregates.ProductAggregate.Services.ProductModificationServices.Variations;

public static class AddReceivedForVariationsService
{

    public static ErrorOr<Product> Execute(Product product, string color, int colorQuantity)
    {
        var colorAssortment = product.Assortments.FirstOrDefault(a => a.Color == color);
        if (colorAssortment == null)
        {
            return Errors.Product.ColorNotFound(color);
        }
        var assortmentTotal = colorAssortment.Sizes.Sum(s => s.Value);
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