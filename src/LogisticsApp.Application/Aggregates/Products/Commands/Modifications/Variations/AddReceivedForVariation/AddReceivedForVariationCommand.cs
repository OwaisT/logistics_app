using ErrorOr;
using LogisticsApp.Domain.BoundedContexts.Catalog.Aggregates.ProductAggregate;
using MediatR;

namespace LogisticsApp.Application.Aggregates.Products.Commands.Modifications.Variations.AddReceivedForVariation;

public record AddReceivedForVariationCommand(
    string ProductId,
    string Color,
    int ColorQuantity
) : IRequest<ErrorOr<Product>>;