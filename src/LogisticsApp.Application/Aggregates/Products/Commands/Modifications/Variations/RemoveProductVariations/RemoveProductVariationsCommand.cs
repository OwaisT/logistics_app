using ErrorOr;
using LogisticsApp.Domain.BoundedContexts.Catalog.Aggregates.ProductAggregate;
using MediatR;

namespace LogisticsApp.Application.Aggregates.Products.Commands.Modifications.Variations.RemoveProductVariations;

public record RemoveProductVariationsCommand(
    string ProductId,
    List<string> VariationIds
) : IRequest<ErrorOr<Product>>;