using ErrorOr;
using LogisticsApp.Domain.BoundedContexts.Catalog.Aggregates.ProductAggregate;
using MediatR;

namespace LogisticsApp.Application.Aggregates.Products.Commands.Modifications.Variations.AddProductVariations;

public record AddProductVariationsCommand(
    string ProductId,
    List<Dictionary<string, string>> ColorSizeCombinations
) : IRequest<ErrorOr<Product>>;