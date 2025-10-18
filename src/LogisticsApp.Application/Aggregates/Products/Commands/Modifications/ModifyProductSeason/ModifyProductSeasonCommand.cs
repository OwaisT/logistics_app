using ErrorOr;
using LogisticsApp.Domain.BoundedContexts.Catalog.Aggregates.ProductAggregate;
using MediatR;

namespace LogisticsApp.Application.Aggregates.Products.Commands.Modifications.ModifyProductSeason;

public record ModifyProductSeasonCommand(
    string ProductId,
    string NewSeason
) : IRequest<ErrorOr<Product>>;