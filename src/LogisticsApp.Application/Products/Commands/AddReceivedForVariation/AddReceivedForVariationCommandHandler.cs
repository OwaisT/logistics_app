using ErrorOr;
using LogisticsApp.Application.Common.Interfaces.Persistence;
using LogisticsApp.Domain.BoundedContexts.Catalog.Aggregates.Product;
using LogisticsApp.Domain.BoundedContexts.Catalog.Aggregates.Product.Services;
using LogisticsApp.Domain.BoundedContexts.Catalog.Aggregates.Product.ValueObjects;
using LogisticsApp.Domain.Common.Errors;
using MediatR;

namespace LogisticsApp.Application.Products.Commands.AddReceivedForVariation;

public class AddReceivedForVariationCommandHandler(
    IProductRepository _productRepository)
    : IRequestHandler<AddReceivedForVariationCommand, ErrorOr<Product>>
{
    public async Task<ErrorOr<Product>> Handle(AddReceivedForVariationCommand command, CancellationToken cancellationToken)
    {
        await Task.CompletedTask;
        var product = _productRepository.GetById(ProductId.Create(Guid.Parse(command.ProductId)));
        if (product is null)
        {
            return Errors.Common.EntityNotFound("Product", command.ProductId);
        }

        var productResult = AddReceivedForVariationsService.Execute(product, command.Color, command.ColorQuantity);
        if (productResult.IsError)
        {
            return productResult.Errors;
        }
        _productRepository.Update(productResult.Value);

        return productResult.Value;
    }
}