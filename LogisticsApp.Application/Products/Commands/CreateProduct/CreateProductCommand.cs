using ErrorOr;
using LogisticsApp.Domain.Aggregates.Product;
using MediatR;

namespace LogisticsApp.Application.Products.Commands.CreateProduct;

public record CreateProductCommand(
    string RefCode,
    string Season,
    string Name,
    string Description,
    bool IsActive,
    List<string> Categories,
    List<string> Colors,
    List<string> Sizes,
    List<AssortmentCommand> Assortments) 
    : IRequest<ErrorOr<Product>>;

public record AssortmentCommand(
    string Color,
    Dictionary<string, int> Sizes); 