using ErrorOr;
using LogisticsApp.Domain.BoundedContexts.Catalog.Aggregates.ProductAggregate;
using MediatR;

namespace LogisticsApp.Application.Aggregates.Products.Commands.Modifications.Variations.AddProductVariations;

public record AddProductVariationsCommand(
    string ProductId,
    List<string> Colors,
    List<string> Sizes
) : IRequest<ErrorOr<Product>>;