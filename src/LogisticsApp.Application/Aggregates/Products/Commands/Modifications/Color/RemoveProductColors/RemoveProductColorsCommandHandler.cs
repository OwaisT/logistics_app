using ErrorOr;
using LogisticsApp.Application.Common.Interfaces.Persistence;
using LogisticsApp.Application.Common.Interfaces.Services;
using LogisticsApp.Domain.BoundedContexts.Catalog.Aggregates.ProductAggregate;
using LogisticsApp.Domain.BoundedContexts.Catalog.Aggregates.ProductAggregate.ValueObjects;
using LogisticsApp.Domain.Common.Errors;
using MediatR;

namespace LogisticsApp.Application.Aggregates.Products.Commands.Modifications.Color.RemoveProductColors;

using RemoveProductColors = Domain.BoundedContexts.Catalog.Aggregates.ProductAggregate.Services.ProductModificationServices.Color.RemoveProductColors;

public class RemoveProductColorsCommandHandler(
    IProductRepository _productRepository,
    IVariationNotUsedChecker _variationNotUsedChecker)
    : IRequestHandler<RemoveProductColorsCommand, ErrorOr<Product>>
{
    public async Task<ErrorOr<Product>> Handle(RemoveProductColorsCommand command, CancellationToken cancellationToken)
    {
        await Task.CompletedTask;
        var product = _productRepository.GetById(ProductId.Create(Guid.Parse(command.ProductId)));
        if (product is null)
        {
            return Errors.Common.EntityNotFound("Product", command.ProductId);
        }
        var notUsedResult = CheckVariationsNotUsed(product, command.Colors);
        if (notUsedResult.IsError)
        {
            return notUsedResult.Errors;
        }

        var productResult = RemoveProductColors.Execute(product, command.Colors, notUsedResult.Value);
        if (productResult.IsError)
        {
            return productResult.Errors;
        }
        _productRepository.Update(productResult.Value);

        return productResult.Value;
    }

    private ErrorOr<List<VariationId>> CheckVariationsNotUsed(Product product, List<string> colorsToRemove)
    {
        var variationsToCheck = product.Variations
            .Where(v => colorsToRemove.Contains(v.Color))
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
