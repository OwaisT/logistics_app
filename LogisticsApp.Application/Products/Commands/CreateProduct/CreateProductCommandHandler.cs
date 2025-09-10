using ErrorOr;
using LogisticsApp.Application.Common.Interfaces.Persistence;
using LogisticsApp.Domain.Aggregates.Product;
using LogisticsApp.Domain.Products.ValueObjects;
using MediatR;

namespace LogisticsApp.Application.Products.Commands.CreateProduct;

public class CreateProductCommandHandler :
    IRequestHandler<CreateProductCommand, ErrorOr<Product>>
{
    private readonly IProductRepository _productRepository;

    public CreateProductCommandHandler(IProductRepository productRepository)
    {
        _productRepository = productRepository;
    }

    public async Task<ErrorOr<Product>> Handle(CreateProductCommand command, CancellationToken cancellationToken)
    {
        await Task.CompletedTask;
        // Logic to create a product would go here.
        var product = Product.Create(
            command.RefCode,
            command.Season,
            command.Name,
            command.Description,
            command.IsActive,
            command.Categories,
            command.Colors,
            command.Sizes,
            command.Assortments.Select(a => Assortment.Create(a.Color, a.Sizes)).ToList());

        _productRepository.Add(product);

        return await Task.FromResult<ErrorOr<Product>>(product);
    }

}
