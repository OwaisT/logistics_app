using ErrorOr;
using LogisticsApp.Domain.BoundedContexts.Catalog.Aggregates.ProductAggregate;
using MediatR;

namespace LogisticsApp.Application.Aggregates.Products.Commands.Modifications.Color.RemoveProductColors;

public record RemoveProductColorsCommand(
    string ProductId,
    List<string> Colors
) : IRequest<ErrorOr<Product>>;