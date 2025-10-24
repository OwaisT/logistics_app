using ErrorOr;
using LogisticsApp.Application.Common.Interfaces.Persistence;
using LogisticsApp.Domain.BoundedContexts.Catalog.Aggregates.DefectiveItemAggregate;
using LogisticsApp.Domain.BoundedContexts.Catalog.Aggregates.ProductAggregate;
using LogisticsApp.Domain.BoundedContexts.Catalog.Aggregates.ProductAggregate.ValueObjects;
using LogisticsApp.Domain.Common.Errors;
using MediatR;

namespace LogisticsApp.Application.Aggregates.DefectiveItems.Commands.CreateDefectiveItem;

public class CreateDefectiveItemCommandHandler(
    IDefectiveItemRepository _defectiveItemRepository,
    IProductRepository _productRepository)
     : IRequestHandler<CreateDefectiveItemCommand, ErrorOr<DefectiveItem>>
{
    public async Task<ErrorOr<DefectiveItem>> Handle(CreateDefectiveItemCommand command, CancellationToken cancellationToken)
    {
        await Task.CompletedTask;
        // Logic to create a defective item would go here.
        var productId = ProductId.Create(Guid.Parse(command.ProductId));
        var product = _productRepository.GetById(productId);
        if (product is null)
        {
            return Errors.Common.EntityNotFound(nameof(Product), command.ProductId.ToString());
        }
        var variationId = VariationId.Create(Guid.Parse(command.VariationId));
        var variationRefCodeResult = VerifyVariationExistenceAndGetRefCode(product, variationId);
        if (variationRefCodeResult.IsError)
        {
            return variationRefCodeResult.Errors;
        }

        var defectiveItem = DefectiveItem.Create(
            productId,
            variationId,
            variationRefCodeResult.Value,
            command.Reason,
            command.IsRepairable
        );
        _defectiveItemRepository.Add(defectiveItem);

        return await Task.FromResult<ErrorOr<DefectiveItem>>(defectiveItem);
    }

    private static ErrorOr<string> VerifyVariationExistenceAndGetRefCode(Product product, VariationId variationId)
    {
        var variation = product.Variations.FirstOrDefault(v => v.Id == variationId);
        if (variation is null)
        {
            return Errors.Common.EntityNotFound("Variation", variationId.Value.ToString());
        }

        return variation.VariationRefCode;
    }

}
