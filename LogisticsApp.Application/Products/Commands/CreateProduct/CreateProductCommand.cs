using ErrorOr;
using LogisticsApp.Application.Products.Common;
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
    List<string> Sizes) 
    : IRequest<ErrorOr<Product>>;
