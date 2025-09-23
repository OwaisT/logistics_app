using ErrorOr;
using LogisticsApp.Domain.BoundedContexts.Catalog.Aggregates.Product;
using MediatR;

namespace LogisticsApp.Application.Products.Queries.GetProducts;

public record GetProductsQuery() : IRequest<ErrorOr<List<Product>>>;