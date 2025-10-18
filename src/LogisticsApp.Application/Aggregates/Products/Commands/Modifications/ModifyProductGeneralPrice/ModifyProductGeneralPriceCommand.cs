using ErrorOr;
using LogisticsApp.Domain.BoundedContexts.Catalog.Aggregates.ProductAggregate;
using MediatR;

namespace LogisticsApp.Application.Aggregates.Products.Commands.Modifications.ModifyProductGeneralPrice;

public record ModifyProductGeneralPriceCommand(
    string ProductId,
    decimal NewPrice,
    bool UpdateVariationsPrices
) : IRequest<ErrorOr<Product>>;