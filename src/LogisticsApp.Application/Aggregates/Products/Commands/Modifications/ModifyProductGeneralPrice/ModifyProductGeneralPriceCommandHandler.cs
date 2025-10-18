using ErrorOr;
using LogisticsApp.Application.Common.Interfaces.Persistence;
using LogisticsApp.Domain.BoundedContexts.Catalog.Aggregates.ProductAggregate;
using LogisticsApp.Domain.BoundedContexts.Catalog.Aggregates.ProductAggregate.ValueObjects;
using LogisticsApp.Domain.Common.Errors;
using MediatR;

namespace LogisticsApp.Application.Aggregates.Products.Commands.Modifications.ModifyProductGeneralPrice;

using ModifyProductGeneralPrice = Domain.BoundedContexts.Catalog.Aggregates.ProductAggregate.Services.ProductModificationServices.ModifyProductGeneralPrice;

public class ModifyProductGeneralPriceCommandHandler(
    IProductRepository _productRepository)
    : IRequestHandler<ModifyProductGeneralPriceCommand, ErrorOr<Product>>
{
    public async Task<ErrorOr<Product>> Handle(ModifyProductGeneralPriceCommand command, CancellationToken cancellationToken)
    {
        await Task.CompletedTask;
        var product = _productRepository.GetById(ProductId.Create(Guid.Parse(command.ProductId)));
        if (product is null)
        {
            return Errors.Common.EntityNotFound("Product", command.ProductId);
        }
        var productResult = ModifyProductGeneralPrice.Execute(product, command.NewPrice, command.UpdateVariationsPrices);
        if (productResult.IsError)
        {
            return productResult.Errors;
        }
        _productRepository.Update(productResult.Value);

        return productResult.Value;
    }
}
