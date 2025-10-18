using ErrorOr;
using LogisticsApp.Domain.BoundedContexts.Catalog.Aggregates.ProductAggregate;
using MediatR;

namespace LogisticsApp.Application.Aggregates.Products.Commands.Modifications.Size.AddProductSizes;

public record AddProductSizesCommand(
    string ProductId,
    List<string> Sizes
) : IRequest<ErrorOr<Product>>;