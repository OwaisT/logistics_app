using ErrorOr;
using LogisticsApp.Application.Common.Interfaces.Persistence;
using LogisticsApp.Domain.BoundedContexts.Catalog.Aggregates.ProductAggregate;
using LogisticsApp.Domain.BoundedContexts.Catalog.Aggregates.ProductAggregate.ValueObjects;
using LogisticsApp.Domain.Common.Errors;
using MediatR;

namespace LogisticsApp.Application.Aggregates.Products.Commands.Modifications.Color.AddProductColors;

using AddProductColors = Domain.BoundedContexts.Catalog.Aggregates.ProductAggregate.Services.ProductModificationServices.Color.AddProductColors;

public class AddProductColorsCommandHandler(
    IProductRepository _productRepository)
    : IRequestHandler<AddProductColorsCommand, ErrorOr<Product>>
{
    public async Task<ErrorOr<Product>> Handle(AddProductColorsCommand command, CancellationToken cancellationToken)
    {
        await Task.CompletedTask;
        var product = _productRepository.GetById(ProductId.Create(Guid.Parse(command.ProductId)));
        if (product is null)
        {
            return Errors.Common.EntityNotFound("Product", command.ProductId);
        }
        var assortments = command.Assortments
            .Select(a => new Assortment(
                a.Color,
                a.Sizes.ToDictionary(kvp => kvp.Key, kvp => kvp.Value)))
            .ToList();
        var productResult = AddProductColors.Execute(product, command.Colors, assortments);
        if (productResult.IsError)
        {
            return productResult.Errors;
        }
        _productRepository.Update(productResult.Value);

        return productResult.Value;
    }
}
