using ErrorOr;
using LogisticsApp.Domain.BoundedContexts.Catalog.Aggregates.ProductAggregate;
using MediatR;

namespace LogisticsApp.Application.Aggregates.Products.Commands.Modifications.Category.AddProductCategories;

public record AddProductCategoriesCommand(
    string ProductId,
    List<string> Categories
    ) : IRequest<ErrorOr<Product>>;