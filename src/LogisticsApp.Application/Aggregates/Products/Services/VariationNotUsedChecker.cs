using LogisticsApp.Application.Common.Interfaces.Persistence;
using LogisticsApp.Application.Common.Interfaces.Services;
using LogisticsApp.Domain.BoundedContexts.Catalog.Aggregates.ProductAggregate.ValueObjects;

namespace LogisticsApp.Application.Aggregates.Products.Services;

public class VariationNotUsedChecker(
    ICartonRepository _cartonRepository,
    IOrderRepository _orderRepository,
    IOrderReturnRepository _orderReturnRepository,
    IDefectiveItemRepository _defectiveItemRepository) : IVariationNotUsedChecker
{
    public bool IsVariationUsed(ProductId productId, VariationId variationId)
    {
        // Check if variation is used in cartons
        if (_cartonRepository.IsVariationUsed(productId, variationId))
        {
            return true;
        }

        // Check if variation is used in orders
        if (_orderRepository.IsVariationUsed(productId, variationId))
        {
            return true;
        }

        // Check if variation is used in order returns
        if (_orderReturnRepository.IsVariationUsed(productId, variationId))
        {
            return true;
        }

        // Check if variation is used in defective items
        if (_defectiveItemRepository.IsVariationUsed(productId, variationId))
        {
            return true;
        }

        return false;
    }
}
