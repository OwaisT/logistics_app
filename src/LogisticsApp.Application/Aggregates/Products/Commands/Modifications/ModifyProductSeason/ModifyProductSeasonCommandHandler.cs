using ErrorOr;
using LogisticsApp.Application.Common.Interfaces.Persistence;
using LogisticsApp.Application.Common.Interfaces.Services;
using LogisticsApp.Domain.BoundedContexts.Catalog.Aggregates.ProductAggregate;
using LogisticsApp.Domain.BoundedContexts.Catalog.Aggregates.ProductAggregate.Services;
using LogisticsApp.Domain.BoundedContexts.Catalog.Aggregates.ProductAggregate.ValueObjects;
using LogisticsApp.Domain.Common.Errors;
using MediatR;

namespace LogisticsApp.Application.Aggregates.Products.Commands.Modifications.ModifyProductSeason;

using ModifyProductSeason = Domain.BoundedContexts.Catalog.Aggregates.ProductAggregate.Services.ProductModificationServices.ModifyProductSeason;

public class ModifyProductSeasonCommandHandler(
    IProductRepository _productRepository,
    IProductNotUsedChecker _productNotUsedChecker,
    IProductUniquenessChecker _productUniquenessChecker)
    : IRequestHandler<ModifyProductSeasonCommand, ErrorOr<Product>>
{
    public async Task<ErrorOr<Product>> Handle(ModifyProductSeasonCommand command, CancellationToken cancellationToken)
    {
        await Task.CompletedTask;
        var productId = ProductId.Create(Guid.Parse(command.ProductId));
        var product = _productRepository.GetById(productId);
        if (product is null)
        {
            return Errors.Common.EntityNotFound("Product", command.ProductId);
        }
        if (_productNotUsedChecker.IsProductUsed(productId))
        {
            return Errors.Product.ProductInUseCannotBeModified(command.ProductId);
        }
        var productResult = ModifyProductSeason.Execute(product, command.NewSeason, _productUniquenessChecker);
        if (productResult.IsError)
        {
            return productResult.Errors;
        }
        _productRepository.Update(productResult.Value);

        return productResult.Value;
    }
}
