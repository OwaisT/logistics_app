using ErrorOr;
using LogisticsApp.Application.Common.Interfaces.Persistence;
using LogisticsApp.Domain.BoundedContexts.Catalog.Aggregates.OrderReturnAggregate;
using LogisticsApp.Domain.BoundedContexts.Catalog.Aggregates.OrderReturnAggregate.ValueObjects;
using LogisticsApp.Domain.Common.Errors;
using MediatR;

namespace LogisticsApp.Application.Aggregates.OrderReturns.Commands.UpdateOrderReturnStatus;

public class UpdateOrderReturnStatusCommandHandler(
    IOrderReturnRepository _orderReturnRepository)
     : IRequestHandler<UpdateOrderReturnStatusCommand, ErrorOr<OrderReturn>>
{
    public async Task<ErrorOr<OrderReturn>> Handle(UpdateOrderReturnStatusCommand command, CancellationToken cancellationToken)
    {
        await Task.CompletedTask;
        var orderReturnId = OrderReturnId.Create(command.OrderReturnId);
        var orderReturn = _orderReturnRepository.GetById(orderReturnId);
        if (orderReturn is null)
        {
            return Errors.Common.EntityNotFound(nameof(OrderReturn), orderReturnId.Value.ToString());
        }
        var updateStatusResult = orderReturn.UpdateStatus(command.Status);
        if (updateStatusResult.IsError)
        {
            return updateStatusResult.Errors;
        }
        _orderReturnRepository.Update(orderReturn);
        return await Task.FromResult<ErrorOr<OrderReturn>>(orderReturn);
    }
}