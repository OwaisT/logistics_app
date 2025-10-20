using ErrorOr;
using LogisticsApp.Domain.BoundedContexts.Catalog.Aggregates.ProductAggregate;
using MediatR;

namespace LogisticsApp.Application.Aggregates.Products.Commands.Modifications.Color.AddProductColors;

public record AddProductColorsCommand(
    string ProductId,
    List<string> Colors,
    List<ProductAssortmentCommand> Assortments
) : IRequest<ErrorOr<Product>>;

public record ProductAssortmentCommand(
    string Color,
    Dictionary<string, int> Sizes);