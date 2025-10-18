using ErrorOr;
using LogisticsApp.Application.Common.Interfaces.Persistence;
using LogisticsApp.Domain.BoundedContexts.Catalog.Aggregates.ProductAggregate;
using LogisticsApp.Domain.BoundedContexts.Catalog.Aggregates.ProductAggregate.ValueObjects;
using LogisticsApp.Domain.Common.Errors;
using MediatR;

namespace LogisticsApp.Application.Aggregates.Products.Commands.Modifications.ModifyProductAssortments;

using ModifyProductAssortments = Domain.BoundedContexts.Catalog.Aggregates.ProductAggregate.Services.ProductModificationServices.ModifyProductAssortments;

public class ModifyProductAssortmentsCommandHandler(
    IProductRepository _productRepository)
    : IRequestHandler<ModifyProductAssortmentsCommand, ErrorOr<Product>>
{
    public async Task<ErrorOr<Product>> Handle(ModifyProductAssortmentsCommand command, CancellationToken cancellationToken)
    {
        await Task.CompletedTask;
        var product = _productRepository.GetById(ProductId.Create(Guid.Parse(command.ProductId)));
        if (product is null)
        {
            return Errors.Common.EntityNotFound("Product", command.ProductId);
        }

        var newAssortments = command.Assortments.Select(a => Assortment.Create(a.Color, a.Sizes)).ToList();
        var productResult = ModifyProductAssortments.Execute(product, newAssortments);
        if (productResult.IsError)
        {
            return productResult.Errors;
        }
        _productRepository.Update(productResult.Value);

        return productResult.Value;
    }
        
}
