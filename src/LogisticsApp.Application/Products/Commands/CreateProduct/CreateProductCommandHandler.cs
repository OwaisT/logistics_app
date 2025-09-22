using ErrorOr;
using LogisticsApp.Application.Common.Interfaces.Persistence;
using LogisticsApp.Domain.Aggregates.Product;
using LogisticsApp.Domain.Products.ValueObjects;
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
        var product = _productFactory.Create(
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

        _productRepository.Add(product);

        return await Task.FromResult<ErrorOr<Product>>(product);
    }

}
