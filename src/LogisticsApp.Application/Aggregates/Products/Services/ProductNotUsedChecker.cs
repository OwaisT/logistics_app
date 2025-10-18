using LogisticsApp.Application.Common.Interfaces.Persistence;
using LogisticsApp.Application.Common.Interfaces.Services;
using LogisticsApp.Domain.BoundedContexts.Catalog.Aggregates.ProductAggregate.ValueObjects;

namespace LogisticsApp.Application.Aggregates.Products.Services;

public class ProductNotUsedChecker(
    ICartonRepository _cartonRepository,
    IOrderRepository _orderRepository,
    IOrderReturnRepository _orderReturnRepository,
    IDefectiveItemRepository _defectiveItemRepository) : IProductNotUsedChecker
{
    public bool IsProductUsed(ProductId productId)
    {
        // Check if product is used in cartons
        if (_cartonRepository.IsProductUsed(productId))
        {
            return true;
        }

        // Check if product is used in orders
        if (_orderRepository.IsProductUsed(productId))
        {
            return true;
        }

        // Check if product is used in order returns
        if (_orderReturnRepository.IsProductUsed(productId))
        {
            return true;
        }

        // Check if product is used in defective items
        if (_defectiveItemRepository.IsProductUsed(productId))
        {
            return true;
        }

        return false;
    }
}