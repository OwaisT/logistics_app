using ErrorOr;
using LogisticsApp.Domain.BoundedContexts.Catalog.Aggregates.OrderReturnAggregate;
using MediatR;

namespace LogisticsApp.Application.Aggregates.OrderReturns.Commands.CreateOrderReturn;

public record CreateOrderReturnCommand(
    Guid OrderId,
    List<Guid> OrderItemIds
) : IRequest<ErrorOr<OrderReturn>>;