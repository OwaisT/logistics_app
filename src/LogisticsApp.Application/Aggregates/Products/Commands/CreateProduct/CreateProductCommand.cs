using ErrorOr;
using LogisticsApp.Domain.BoundedContexts.Catalog.Aggregates.Product;
using MediatR;

namespace LogisticsApp.Application.Aggregates.Products.Commands.CreateProduct;

public record CreateProductCommand(
    string RefCode,
    string Season,
    string Name,
    string Description,
    decimal GeneralPrice,
    bool IsActive,
    List<string> Categories,
    List<string> Colors,
    List<string> Sizes,
    List<CreateProductAssortmentCommand> Assortments) 
    : IRequest<ErrorOr<Product>>;

public record CreateProductAssortmentCommand(
    string Color,
    Dictionary<string, int> Sizes); 