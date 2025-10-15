using ErrorOr;
using LogisticsApp.Domain.BoundedContexts.Catalog.Aggregates.ProductAggregate;
using MediatR;

namespace LogisticsApp.Application.Aggregates.Products.Commands.AddProductColor;

public record AddProductColorCommand(
    Guid ProductId,
    string Color
) : IRequest<ErrorOr<Product>>;
