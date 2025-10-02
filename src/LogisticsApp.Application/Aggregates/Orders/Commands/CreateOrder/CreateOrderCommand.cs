using ErrorOr;
using LogisticsApp.Domain.BoundedContexts.Catalog.Aggregates.OrderAggregate;
using MediatR;

namespace LogisticsApp.Application.Aggregates.Orders.Commands.CreateOrder;

public record CreateOrderCommand(
    decimal TotalValue,
    List<OrderItemCommand> Items) : IRequest<ErrorOr<Order>>;

public record OrderItemCommand(
    string ProductId,
    string VariationId,
    int Quantity
);