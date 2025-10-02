using ErrorOr;
using LogisticsApp.Application.Common.Interfaces.Persistence;
using LogisticsApp.Application.Common.Services;
using LogisticsApp.Domain.BoundedContexts.Catalog.Aggregates.OrderAggregate;
using LogisticsApp.Domain.BoundedContexts.Catalog.Aggregates.OrderAggregate.Entities;
using LogisticsApp.Domain.BoundedContexts.Catalog.Aggregates.OrderAggregate.Services;
using LogisticsApp.Domain.BoundedContexts.Catalog.Aggregates.ProductAggregate.ValueObjects;
using MediatR;

namespace LogisticsApp.Application.Aggregates.Orders.Commands.CreateOrder;

public class CreateOrderCommandHandler(
    IOrderRepository _orderRepository,
    EnforceProductInvariantsAndGetVariationRefCodeService _enforceProductInvariantsAndGetVariationRefCodeService,
    OrderCreationService _orderCreationService)
     : IRequestHandler<CreateOrderCommand, ErrorOr<Order>>
{
    public async Task<ErrorOr<Order>> Handle(CreateOrderCommand command, CancellationToken cancellationToken)
    {
        await Task.CompletedTask;
        var orderItems = new List<OrderItem>();
        foreach (var item in command.Items)
        {
            var productId = ProductId.Create(Guid.Parse(item.ProductId));
            var variationId = VariationId.Create(Guid.Parse(item.VariationId));
            var variationRefCodeResult = _enforceProductInvariantsAndGetVariationRefCodeService.Enforce(productId, variationId);
            if (variationRefCodeResult.IsError)
            {
                return variationRefCodeResult.Errors;
            }
            for (int i = 0; i < item.Quantity; i++)
            {
                var orderItem = OrderItem.Create(
                    ProductId.Create(Guid.Parse(item.ProductId)),
                    VariationId.Create(Guid.Parse(item.VariationId)),
                    variationRefCodeResult.Value);
                orderItems.Add(orderItem);
            }
        }

        var orderResult = _orderCreationService.CreateOrder(
            orderItems,
            command.TotalValue
        );
        if (orderResult.IsError)
        {
            return orderResult.Errors;
        }
        var order = orderResult.Value;
        _orderRepository.Add(order);

        return await Task.FromResult<ErrorOr<Order>>(order);
    }
}