using ErrorOr;
using LogisticsApp.Domain.BoundedContexts.Catalog.Aggregates.ProductAggregate;
using MediatR;

namespace LogisticsApp.Application.Aggregates.Products.Commands.Modifications.Size.RemoveProductSizes;

public record RemoveProductSizesCommand(
    string ProductId,
    List<string> Sizes
) : IRequest<ErrorOr<Product>>;