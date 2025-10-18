using ErrorOr;
using LogisticsApp.Domain.BoundedContexts.Catalog.Aggregates.ProductAggregate;
using MediatR;

namespace LogisticsApp.Application.Aggregates.Products.Commands.Modifications.Category.RemoveProductCategories;

public record RemoveProductCategoriesCommand(
    string ProductId,
    List<string> Categories
    ) : IRequest<ErrorOr<Product>>;