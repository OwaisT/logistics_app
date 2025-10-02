using ErrorOr;
using LogisticsApp.Domain.BoundedContexts.Catalog.Aggregates.OrderReturnAggregate;
using MediatR;

namespace LogisticsApp.Application.Aggregates.OrderReturns.Commands.UpdateOrderReturnStatus;

public record UpdateOrderReturnStatusCommand(
    Guid OrderReturnId,
    string Status
) : IRequest<ErrorOr<OrderReturn>>;
