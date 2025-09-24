using ErrorOr;
using LogisticsApp.Application.Common.Interfaces.Persistence;
using LogisticsApp.Domain.BoundedContexts.Catalog.Aggregates.Product;
using LogisticsApp.Domain.BoundedContexts.Catalog.Aggregates.Product.ValueObjects;
using LogisticsApp.Domain.BoundedContexts.Positioning.Aggregates.Carton;
using LogisticsApp.Domain.BoundedContexts.Positioning.Aggregates.Carton.ValueObjects;
using LogisticsApp.Domain.Common.Errors;
using MediatR;

namespace LogisticsApp.Application.Cartons.Commands.AddCartonItem;

public class AddCartonItemCommandHandler(ICartonRepository _cartonRepository, IProductRepository _productRepository) :
    IRequestHandler<AddCartonItemCommand, ErrorOr<Carton>>
{
    public async Task<ErrorOr<Carton>> Handle(AddCartonItemCommand command, CancellationToken cancellationToken)
    {
        await Task.CompletedTask;
        var cartonResult = EnforceCartonInvariants(CartonId.Create(Guid.Parse(command.CartonId)));
        if (cartonResult.IsError)
        {
            return cartonResult.Errors;
        }
        var productId = ProductId.Create(Guid.Parse(command.ProductId));
        var variationId = VariationId.Create(Guid.Parse(command.VariationId));
        var variationRefCodeResult = EnforceProductInvariantsAndGetVariationRefCode(productId, variationId);
        if (variationRefCodeResult.IsError)
        {
            return variationRefCodeResult.Errors;
        }
        cartonResult.Value.AddItem(productId, variationId, variationRefCodeResult.Value, command.Quantity);

        return await Task.FromResult<ErrorOr<Carton>>(cartonResult.Value);
    }

    private ErrorOr<Carton> EnforceCartonInvariants(CartonId cartonId)
    {
        // Check invariants for adding a carton item
        if (cartonId == null)
        {
            return Errors.Common.InvalidInput("Invalid carton information.");
        }
        var carton = _cartonRepository.GetById(cartonId);
        if (carton is null)
        {
            return Errors.Common.EntityNotFound(nameof(Carton), cartonId.Value.ToString());
        }
        return carton;
    }

    private ErrorOr<string> EnforceProductInvariantsAndGetVariationRefCode(ProductId productId, VariationId variationId)
    {
        if (productId == null || variationId == null)
        {
            return Errors.Common.InvalidInput("Invalid product or variation information.");
        }
        var product = _productRepository.GetById(productId);
        if (product is null)
        {
            return Errors.Common.EntityNotFound(nameof(Product), productId.Value.ToString());
        }
        var variation = product.GetVariation(variationId);
        if (variation is null)
        {
            return Errors.Common.EntityNotFound(nameof(Product) + " Variation", variationId.Value.ToString());
        }
        return variation.VariationRefCode;
    }
}