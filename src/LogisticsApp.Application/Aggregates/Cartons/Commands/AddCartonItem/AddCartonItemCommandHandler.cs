using ErrorOr;
using LogisticsApp.Application.Common.Interfaces.Persistence;
using LogisticsApp.Application.Common.Interfaces.Services;
using LogisticsApp.Domain.BoundedContexts.Catalog.Aggregates.ProductAggregate.ValueObjects;
using LogisticsApp.Domain.BoundedContexts.Positioning.Aggregates.Carton;
using LogisticsApp.Domain.BoundedContexts.Positioning.Aggregates.Carton.Services;
using LogisticsApp.Domain.BoundedContexts.Positioning.Aggregates.Carton.ValueObjects;
using LogisticsApp.Domain.Common.Errors;
using MediatR;

namespace LogisticsApp.Application.Aggregates.Cartons.Commands.AddCartonItem;

public class AddCartonItemCommandHandler(
    ICartonRepository _cartonRepository,
    IVariationExistenceChecker _variationExistenceChecker)
     : IRequestHandler<AddCartonItemCommand, ErrorOr<Carton>>
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
        var variationRefCodeResult = _variationExistenceChecker.ValidateVariationExistenceAndGetRefCode(productId, variationId);
        if (variationRefCodeResult.IsError)
        {
            return variationRefCodeResult.Errors;
        }
        var addItemResult = AddCartonItemService.Execute(cartonResult.Value, productId, variationId, variationRefCodeResult.Value, command.Quantity);
        if (addItemResult.IsError)
        {
            return addItemResult.Errors;
        }
        _cartonRepository.Update(addItemResult.Value);

        return await Task.FromResult<ErrorOr<Carton>>(addItemResult.Value);
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
}