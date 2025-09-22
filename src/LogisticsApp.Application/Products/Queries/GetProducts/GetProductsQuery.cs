using ErrorOr;
using LogisticsApp.Domain.Aggregates.Product;
using MediatR;

namespace LogisticsApp.Application.Products.Queries.GetProducts;

public record GetProductsQuery() : IRequest<ErrorOr<List<Product>>>;