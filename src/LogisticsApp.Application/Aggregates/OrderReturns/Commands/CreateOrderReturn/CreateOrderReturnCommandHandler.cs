using ErrorOr;
using LogisticsApp.Application.Common.Interfaces.Persistence;
using LogisticsApp.Application.Common.Interfaces.Services;
using LogisticsApp.Domain.BoundedContexts.Catalog.Aggregates.OrderAggregate.ValueObjects;
using LogisticsApp.Domain.BoundedContexts.Catalog.Aggregates.OrderReturnAggregate;
using LogisticsApp.Domain.BoundedContexts.Catalog.Aggregates.OrderReturnAggregate.Entities;
using MediatR;

namespace LogisticsApp.Application.Aggregates.OrderReturns.Commands.CreateOrderReturn;

public class CreateOrderReturnCommandHandler(
    IOrderReturnRepository _orderReturnRepository,
    IOrderReturnItemsValidation _orderReturnItemsValidation)
     : IRequestHandler<CreateOrderReturnCommand, ErrorOr<OrderReturn>>
{
    public async Task<ErrorOr<OrderReturn>> Handle(CreateOrderReturnCommand command, CancellationToken cancellationToken)
    {
        await Task.CompletedTask;
        var orderId = OrderId.Create(command.OrderId);
        var orderReturnItemsValidationResult = _orderReturnItemsValidation.ValidateAndGetOrderItemsForReturn(orderId, command.OrderItemIds);
        if (orderReturnItemsValidationResult.IsError)
        {
            return orderReturnItemsValidationResult.Errors;
        }
        var orderReturnItems = orderReturnItemsValidationResult.Value
            .Select(oi => OrderReturnItem.Create(oi.Id, oi.ProductId, oi.VariationId, oi.RefCode))
            .ToList();
        var orderReturn = OrderReturn.Create(
            orderId,
            orderReturnItems,
            "Pending");
        _orderReturnRepository.Add(orderReturn);

        return await Task.FromResult<ErrorOr<OrderReturn>>(orderReturn);
    }
}