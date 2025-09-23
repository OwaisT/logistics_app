using ErrorOr;
using LogisticsApp.Application.Common.Interfaces.Persistence;
using LogisticsApp.Domain.BoundedContexts.Catalog.Aggregates.Product;
using LogisticsApp.Domain.BoundedContexts.Catalog.Aggregates.Product.ValueObjects;
using MediatR;

namespace LogisticsApp.Application.Products.Commands.CreateProduct;

public class CreateProductCommandHandler(IProductRepository productRepository, ProductFactory productFactory) :
    IRequestHandler<CreateProductCommand, ErrorOr<Product>>
{
    private readonly IProductRepository _productRepository = productRepository;
    private readonly ProductFactory _productFactory = productFactory;

    public async Task<ErrorOr<Product>> Handle(CreateProductCommand command, CancellationToken cancellationToken)
    {
        await Task.CompletedTask;
        // Logic to create a product would go here.
        var productResult = _productFactory.Create(
            command.RefCode,
            command.Season,
            command.Name,
            command.Description,
            command.GeneralPrice,
            command.IsActive,
            command.Categories,
            command.Colors,
            command.Sizes,
            command.Assortments.Select(a => Assortment.Create(a.Color, a.Sizes)).ToList());
        
        if (productResult.IsError)
        {
            return productResult.Errors;
        }

        _productRepository.Add(productResult.Value);

        return await Task.FromResult<ErrorOr<Product>>(productResult.Value);
    }

}
