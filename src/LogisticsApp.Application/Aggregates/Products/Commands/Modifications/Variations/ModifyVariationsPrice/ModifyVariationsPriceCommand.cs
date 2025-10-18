using ErrorOr;
using LogisticsApp.Domain.BoundedContexts.Catalog.Aggregates.ProductAggregate;
using MediatR;

namespace LogisticsApp.Application.Aggregates.Products.Commands.Modifications.Variations.ModifyVariationsPrice;

public record ModifyVariationsPriceCommand(
    string ProductId,
    decimal NewPrice,
    string Color
) : IRequest<ErrorOr<Product>>;