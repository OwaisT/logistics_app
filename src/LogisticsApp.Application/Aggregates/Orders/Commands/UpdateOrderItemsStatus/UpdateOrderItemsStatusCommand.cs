using ErrorOr;
using LogisticsApp.Domain.BoundedContexts.Catalog.Aggregates.OrderAggregate;
using MediatR;

namespace LogisticsApp.Application.Aggregates.Orders.Commands.UpdateOrderItemsStatus;
public record UpdateOrderItemsStatusCommand(
    Guid OrderId,
    List<Guid> OrderItemsIds,
    string Status
) : IRequest<ErrorOr<Order>>;