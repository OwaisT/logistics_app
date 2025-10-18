using ErrorOr;
using LogisticsApp.Domain.BoundedContexts.Catalog.Aggregates.ProductAggregate;
using MediatR;

namespace LogisticsApp.Application.Aggregates.Products.Commands.Modifications.ModifyProductAssortments;

public record ModifyProductAssortmentsCommand(
    string ProductId,
    List<ModifyProductAssortmentCommand> Assortments
) : IRequest<ErrorOr<Product>>;

public record ModifyProductAssortmentCommand(
    string Color,
    Dictionary<string, int> Sizes);