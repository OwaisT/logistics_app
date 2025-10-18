using ErrorOr;
using LogisticsApp.Application.Common.Interfaces.Persistence;
using LogisticsApp.Application.Common.Interfaces.Services;
using LogisticsApp.Domain.BoundedContexts.Catalog.Aggregates.ProductAggregate;
using LogisticsApp.Domain.BoundedContexts.Catalog.Aggregates.ProductAggregate.ValueObjects;
using LogisticsApp.Domain.Common.Errors;
using MediatR;

namespace LogisticsApp.Application.Aggregates.Products.Commands.Modifications.Size.RemoveProductSizes;

using RemoveProductSizes = Domain.BoundedContexts.Catalog.Aggregates.ProductAggregate.Services.ProductModificationServices.Size.RemoveProductSizes;

public class RemoveProductSizesCommandHandler(
    IProductRepository _productRepository,
    IVariationNotUsedChecker _variationNotUsedChecker)
    : IRequestHandler<RemoveProductSizesCommand, ErrorOr<Product>>
{
    public async Task<ErrorOr<Product>> Handle(RemoveProductSizesCommand command, CancellationToken cancellationToken)
    {
        await Task.CompletedTask;
        var product = _productRepository.GetById(ProductId.Create(Guid.Parse(command.ProductId)));
        if (product is null)
        {
            return Errors.Common.EntityNotFound("Product", command.ProductId);
        }
        var notUsedResult = CheckVariationsNotUsed(product, command.Sizes);
        if (notUsedResult.IsError)
        {
            return notUsedResult.Errors;
        }

        var productResult = RemoveProductSizes.Execute(product, command.Sizes, notUsedResult.Value);
        if (productResult.IsError)
        {
            return productResult.Errors;
        }
        _productRepository.Update(productResult.Value);

        return productResult.Value;
    }

    private ErrorOr<List<VariationId>> CheckVariationsNotUsed(Product product, List<string> sizesToRemove)
    {
        var variationsToCheck = product.Variations
            .Where(v => sizesToRemove.Contains(v.Size))
            .ToList();

        foreach (var variation in variationsToCheck)
        {
            if (_variationNotUsedChecker.IsVariationUsed(product.Id, variation.Id))
            {
                return Errors.Product.VariationInUse(variation.Color, variation.Size);
            }
        }
        var variationsToRemoveIds = variationsToCheck.Select(v => v.Id).ToList();
        return variationsToRemoveIds;
    }
        
}
