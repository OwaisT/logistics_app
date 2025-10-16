using ErrorOr;
using LogisticsApp.Application.Aggregates.OrderReturns.Commands.CreateOrderReturn;
using LogisticsApp.Application.Common.Interfaces.Persistence;
using LogisticsApp.Domain.BoundedContexts.Catalog.Aggregates.OrderAggregate.ValueObjects;
using LogisticsApp.Domain.BoundedContexts.Catalog.Aggregates.OrderReturnAggregate;
using LogisticsApp.Domain.BoundedContexts.Catalog.Aggregates.OrderReturnAggregate.Services;
using LogisticsApp.Domain.Common.Errors;
using MediatR;

namespace LogisticsApp.Application.Aggregates.OrderReturns.CreateOrderReturn;

public class CreateOrderReturnCommandHandler(
    IOrderReturnRepository _orderReturnRepository,
    OrderReturnCreationService _orderReturnCreationService)
     : IRequestHandler<CreateOrderReturnCommand, ErrorOr<OrderReturn>>
{
    public async Task<ErrorOr<OrderReturn>> Handle(CreateOrderReturnCommand command, CancellationToken cancellationToken)
    {
        await Task.CompletedTask;
        var orderId = OrderId.Create(command.OrderId);
        var orderReturnResult = _orderReturnCreationService.CreateOrderReturn(orderId, command.OrderItemIds);
        if (orderReturnResult.IsError)
        {
            return orderReturnResult.Errors;
        }
        _orderReturnRepository.Add(orderReturnResult.Value);

        return await Task.FromResult<ErrorOr<OrderReturn>>(orderReturnResult.Value);
    }
}