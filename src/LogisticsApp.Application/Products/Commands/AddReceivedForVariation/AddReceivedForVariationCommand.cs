using ErrorOr;
using LogisticsApp.Domain.BoundedContexts.Catalog.Aggregates.Product;
using MediatR;

namespace LogisticsApp.Application.Products.Commands.AddReceivedForVariation;

public record AddReceivedForVariationCommand(
    string ProductId,
    string Color,
    int ColorQuantity
) : IRequest<ErrorOr<Product>>;