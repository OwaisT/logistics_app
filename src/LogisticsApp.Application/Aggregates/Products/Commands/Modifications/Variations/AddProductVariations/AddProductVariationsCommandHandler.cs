using ErrorOr;
using LogisticsApp.Application.Common.Interfaces.Persistence;
using LogisticsApp.Domain.BoundedContexts.Catalog.Aggregates.ProductAggregate;
using LogisticsApp.Domain.BoundedContexts.Catalog.Aggregates.ProductAggregate.ValueObjects;
using LogisticsApp.Domain.Common.Errors;
using MediatR;

namespace LogisticsApp.Application.Aggregates.Products.Commands.Modifications.Variations.AddProductVariations;

using AddProductVariations = Domain.BoundedContexts.Catalog.Aggregates.ProductAggregate.Services.ProductModificationServices.Variations.AddProductVariations;

public class AddProductVariationsCommandHandler(
    IProductRepository _productRepository)
    : IRequestHandler<AddProductVariationsCommand, ErrorOr<Product>>
{
    public async Task<ErrorOr<Product>> Handle(AddProductVariationsCommand command, CancellationToken cancellationToken)
    {
        await Task.CompletedTask;
        var product = _productRepository.GetById(ProductId.Create(Guid.Parse(command.ProductId)));
        if (product is null)
        {
            return Errors.Common.EntityNotFound("Product", command.ProductId);
        }
        var productResult = AddProductVariations.Execute(product, command.ColorSizeCombinations);
        if (productResult.IsError)
        {
            return productResult.Errors;
        }
        _productRepository.Update(productResult.Value);

        return productResult.Value;
    }
}
