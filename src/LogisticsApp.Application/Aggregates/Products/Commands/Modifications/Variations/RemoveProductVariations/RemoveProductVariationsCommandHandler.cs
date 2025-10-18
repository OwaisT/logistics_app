using ErrorOr;
using LogisticsApp.Application.Aggregates.Products.Commands.Modifications.Color.RemoveProductColors;
using LogisticsApp.Application.Common.Interfaces.Persistence;
using LogisticsApp.Application.Common.Interfaces.Services;
using LogisticsApp.Domain.BoundedContexts.Catalog.Aggregates.ProductAggregate;
using LogisticsApp.Domain.BoundedContexts.Catalog.Aggregates.ProductAggregate.ValueObjects;
using LogisticsApp.Domain.Common.Errors;
using MediatR;

namespace LogisticsApp.Application.Aggregates.Products.Commands.Modifications.Variations.RemoveProductVariations;

using RemoveProductVariations = Domain.BoundedContexts.Catalog.Aggregates.ProductAggregate.Services.ProductModificationServices.Variations.RemoveProductVariations;

public class RemoveProductVariationsCommandHandler(
    IProductRepository _productRepository,
    IVariationNotUsedChecker _variationNotUsedChecker)
    : IRequestHandler<RemoveProductVariationsCommand, ErrorOr<Product>>
{
    public async Task<ErrorOr<Product>> Handle(RemoveProductVariationsCommand command, CancellationToken cancellationToken)
    {
        await Task.CompletedTask;
        var product = _productRepository.GetById(ProductId.Create(Guid.Parse(command.ProductId)));
        if (product is null)
        {
            return Errors.Common.EntityNotFound("Product", command.ProductId);
        }
        var notUsedResult = CheckVariationsNotUsed(product, command.VariationIds);
        if (notUsedResult.IsError)
        {
            return notUsedResult.Errors;
        }

        var productResult = RemoveProductVariations.Execute(product, notUsedResult.Value);
        if (productResult.IsError)
        {
            return productResult.Errors;
        }
        _productRepository.Update(productResult.Value);

        return productResult.Value;
    }

    private ErrorOr<List<VariationId>> CheckVariationsNotUsed(Product product, List<string> variationsToCheckIdsString)
    {
        // TODO: Create IDs from strings in a safer way
        var variationsToCheck = product.Variations
            .Where(v => variationsToCheckIdsString.Contains(v.Id.ToString()!))
            .ToList();
        
        foreach (var variation in variationsToCheck)
        {
            if (_variationNotUsedChecker.IsVariationUsed(product.Id, variation.Id))
            {
                return Errors.Product.VariationInUse(variation.Color, variation.Size);
            }
        }
        return variationsToCheck.Select(v => v.Id).ToList();
    }
        
}
