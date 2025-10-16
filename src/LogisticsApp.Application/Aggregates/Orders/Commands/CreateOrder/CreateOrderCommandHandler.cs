using ErrorOr;
using LogisticsApp.Application.Common.Interfaces.Persistence;
using LogisticsApp.Application.Common.Interfaces.Services;
using LogisticsApp.Domain.BoundedContexts.Catalog.Aggregates.OrderAggregate;
using LogisticsApp.Domain.BoundedContexts.Catalog.Aggregates.OrderAggregate.Entities;
using LogisticsApp.Domain.BoundedContexts.Catalog.Aggregates.ProductAggregate.ValueObjects;
using MediatR;

namespace LogisticsApp.Application.Aggregates.Orders.Commands.CreateOrder;

public class CreateOrderCommandHandler(
    IOrderRepository _orderRepository,
    IProductAvailabilityChecker _productAvailabilityChecker)
     : IRequestHandler<CreateOrderCommand, ErrorOr<Order>>
{
    public async Task<ErrorOr<Order>> Handle(CreateOrderCommand command, CancellationToken cancellationToken)
    {
        await Task.CompletedTask;
        var orderItems = ValidateAndCreateOrderItems(command);

        var order = Order.Create(
            orderItems,
            command.TotalValue);
        _orderRepository.Add(order);

        return await Task.FromResult<ErrorOr<Order>>(order);
    }

    private List<OrderItem> ValidateAndCreateOrderItems(CreateOrderCommand command)
    {
        var orderItems = new List<OrderItem>();
        foreach (var item in command.Items)
        {
            var productId = ProductId.Create(Guid.Parse(item.ProductId));
            var variationId = VariationId.Create(Guid.Parse(item.VariationId));
            var quantity = item.Quantity;

            var availabilityResult = _productAvailabilityChecker.ValidateProductAvailabilityAndGetVariationRefCode(productId, variationId, quantity);
            if (availabilityResult.IsError)
            {
                throw new Exception(string.Join(", ", availabilityResult.Errors.Select(e => e.Description)));
            }
            var variationRefCode = availabilityResult.Value;

            for (int i = 0; i < quantity; i++)
            {
                var orderItem = OrderItem.Create(
                    productId,
                    variationId,
                    variationRefCode);

                orderItems.Add(orderItem);
            }

        }
        return orderItems;
    }
}