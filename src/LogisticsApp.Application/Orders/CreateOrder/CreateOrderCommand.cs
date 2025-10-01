using ErrorOr;
using LogisticsApp.Domain.BoundedContexts.Catalog.Aggregates.Order;
using MediatR;

namespace LogisticsApp.Application.Orders.CreateOrder;

public record CreateOrderCommand(
    decimal TotalValue,
    List<OrderItemCommand> Items) : IRequest<ErrorOr<Order>>;

public record OrderItemCommand(
    string ProductId,
    string VariationId,
    int Quantity
);