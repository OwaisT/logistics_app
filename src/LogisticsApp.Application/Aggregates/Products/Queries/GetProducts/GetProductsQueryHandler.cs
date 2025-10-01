using ErrorOr;
using LogisticsApp.Application.Common.Interfaces.Persistence;
using LogisticsApp.Domain.BoundedContexts.Catalog.Aggregates.Product;
using MediatR;

namespace LogisticsApp.Application.Aggregates.Products.Queries.GetProducts;

public class GetProductsQueryHandler(IProductRepository productRepository) :
    IRequestHandler<GetProductsQuery, ErrorOr<List<Product>>>
{
    private readonly IProductRepository _productRepository = productRepository;
    
    public async Task<ErrorOr<List<Product>>> Handle(GetProductsQuery request, CancellationToken cancellationToken)
    {
        var products = await _productRepository.GetAllAsync();
        return ErrorOrFactory.From(products);
    }
}