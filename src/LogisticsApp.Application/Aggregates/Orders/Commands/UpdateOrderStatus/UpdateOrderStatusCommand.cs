using ErrorOr;
using LogisticsApp.Domain.BoundedContexts.Catalog.Aggregates.Order;
using MediatR;

namespace LogisticsApp.Application.Aggregates.Orders.Commands.UpdateOrderStatus;

public record UpdateOrderStatusCommand(
    Guid OrderId,
    string Status
) : IRequest<ErrorOr<Order>>;