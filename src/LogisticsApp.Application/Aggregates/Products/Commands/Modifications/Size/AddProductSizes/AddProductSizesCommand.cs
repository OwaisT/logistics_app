using ErrorOr;
using LogisticsApp.Domain.BoundedContexts.Catalog.Aggregates.ProductAggregate;
using MediatR;

namespace LogisticsApp.Application.Aggregates.Products.Commands.Modifications.Size.AddProductSizes;

public record AddProductSizesCommand(
    string ProductId,
    List<string> Sizes,
    List<ProductAssortmentCommand> Assortments
) : IRequest<ErrorOr<Product>>;

public record ProductAssortmentCommand(
    string Color,
    Dictionary<string, int> Sizes);