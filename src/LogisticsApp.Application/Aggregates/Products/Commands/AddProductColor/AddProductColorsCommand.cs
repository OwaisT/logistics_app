using ErrorOr;
using LogisticsApp.Domain.BoundedContexts.Catalog.Aggregates.ProductAggregate;
using MediatR;

namespace LogisticsApp.Application.Aggregates.Products.Commands.AddProductColor;

public record AddProductColorsCommand(
    string ProductId,
    List<string> Colors
) : IRequest<ErrorOr<Product>>;
