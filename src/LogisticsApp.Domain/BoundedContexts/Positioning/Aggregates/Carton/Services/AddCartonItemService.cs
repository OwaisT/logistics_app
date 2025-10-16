using ErrorOr;
using LogisticsApp.Domain.BoundedContexts.Catalog.Aggregates.ProductAggregate.ValueObjects;
using LogisticsApp.Domain.Common.Errors;

namespace LogisticsApp.Domain.BoundedContexts.Positioning.Aggregates.Carton.Services;

public static class AddCartonItemService
{
    // Implementation details
    public static ErrorOr<Carton> Execute(Carton carton, ProductId productId, VariationId variationId, string refCode, int quantity)
    {
        if (quantity <= 0)
        {
            return Errors.Common.CannotBeNegativeOrZero(nameof(quantity));
        }
        return carton.AddItem(productId, variationId, refCode, quantity);
    }
}