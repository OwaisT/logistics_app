using ErrorOr;
using LogisticsApp.Domain.BoundedContexts.Catalog.Aggregates.Order.Entities;
using LogisticsApp.Domain.BoundedContexts.Catalog.Services;
using LogisticsApp.Domain.Common.Errors;

namespace LogisticsApp.Domain.BoundedContexts.Catalog.Aggregates.Order;

public class OrderFactory(
    IProductAvailabilityChecker _productAvailabilityChecker)
{
    public ErrorOr<Order> CreateOrder(
        List<OrderItem> items,
        decimal totalValue
    )
    {
        foreach (var item in items)
            {
                // Check stock availability for each item
                var isAvailable = _productAvailabilityChecker.IsProductVariationInStock(item.ProductId, item.VariationId);

                // If any item is out of stock, return an error
                if (!isAvailable)
                {
                    return Errors.Order.OutOfStock(item.RefCode);
                }
            }

        return Order.Create(
                items,
                totalValue
            );
    }
}