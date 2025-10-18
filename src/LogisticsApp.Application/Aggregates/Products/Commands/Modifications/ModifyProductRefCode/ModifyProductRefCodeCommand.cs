using ErrorOr;
using LogisticsApp.Domain.BoundedContexts.Catalog.Aggregates.ProductAggregate;
using MediatR;

namespace LogisticsApp.Application.Aggregates.Products.Commands.Modifications.ModifyProductRefCode;

public record ModifyProductRefCodeCommand(
    string ProductId,
    string NewRefCode
) : IRequest<ErrorOr<Product>>;