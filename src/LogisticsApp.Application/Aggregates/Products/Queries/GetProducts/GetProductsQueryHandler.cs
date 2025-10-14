using ErrorOr;
using LogisticsApp.Application.Common.Interfaces.Persistence;
using LogisticsApp.Domain.BoundedContexts.Catalog.Aggregates.ProductAggregate;
using MediatR;

namespace LogisticsApp.Application.Aggregates.Products.Queries.GetProducts;

public class GetProductsQueryHandler(IProductRepository productRepository) :
    IRequestHandler<GetProductsQuery, ErrorOr<List<Product>>>
{
    private readonly IProductRepository _productRepository = productRepository;
    
    public async Task<ErrorOr<List<Product>>> Handle(GetProductsQuery request, CancellationToken cancellationToken)
    {
        await Task.CompletedTask;
        var products = _productRepository.GetAll();
        return ErrorOrFactory.From(products);
    }
}