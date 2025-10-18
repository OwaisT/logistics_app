using ErrorOr;
using LogisticsApp.Application.Common.Interfaces.Persistence;
using LogisticsApp.Domain.BoundedContexts.Catalog.Aggregates.ProductAggregate;
using LogisticsApp.Domain.BoundedContexts.Catalog.Aggregates.ProductAggregate.ValueObjects;
using LogisticsApp.Domain.Common.Errors;
using MediatR;

namespace LogisticsApp.Application.Aggregates.Products.Commands.Modifications.Category.AddProductCategories;

using AddProductCategories = Domain.BoundedContexts.Catalog.Aggregates.ProductAggregate.Services.ProductModificationServices.Category.AddProductCategories;

public class AddProductCategoriesCommandHandler(
    IProductRepository _productRepository)
    : IRequestHandler<AddProductCategoriesCommand, ErrorOr<Product>>
{
    public async Task<ErrorOr<Product>> Handle(AddProductCategoriesCommand command, CancellationToken cancellationToken)
    {
        await Task.CompletedTask;
        var product = _productRepository.GetById(ProductId.Create(Guid.Parse(command.ProductId)));
        if (product is null)
        {
            return Errors.Common.EntityNotFound("Product", command.ProductId);
        }
        var productResult = AddProductCategories.Execute(product, command.Categories);
        if (productResult.IsError)
        {
            return productResult.Errors;
        }
        _productRepository.Update(productResult.Value);

        return productResult.Value;
    }
}
