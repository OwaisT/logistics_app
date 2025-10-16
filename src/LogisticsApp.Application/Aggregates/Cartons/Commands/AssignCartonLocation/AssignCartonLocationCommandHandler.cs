using ErrorOr;
using LogisticsApp.Application.Common.Interfaces.Persistence;
using LogisticsApp.Application.Common.Interfaces.Services;
using LogisticsApp.Domain.BoundedContexts.Positioning.Aggregates.Carton;
using LogisticsApp.Domain.BoundedContexts.Positioning.Aggregates.Carton.Services;
using LogisticsApp.Domain.BoundedContexts.Positioning.Aggregates.Carton.ValueObjects;
using LogisticsApp.Domain.BoundedContexts.Positioning.Aggregates.Warehouse.ValueObjects;
using LogisticsApp.Domain.Common.Errors;
using MediatR;

namespace LogisticsApp.Application.Aggregates.Cartons.Commands.AssignCartonLocation;

public class AssignCartonLocationCommandHandler(
    ICartonRepository _cartonRepository,
    CartonLocationAssigner _cartonLocationAssigner,
    IWarehouseAndRoomExistenceChecker _warehouseAndRoomExistenceChecker) : IRequestHandler<AssignCartonLocationCommand, ErrorOr<Carton>>
{
    public async Task<ErrorOr<Carton>> Handle(AssignCartonLocationCommand command, CancellationToken cancellationToken)
    {
        await Task.CompletedTask;
        var cartonResult = EnforceCartonInvariants(CartonId.Create(Guid.Parse(command.CartonId)));
        if (cartonResult.IsError)
        {
            return cartonResult.Errors;
        }
        var warehouseId = WarehouseId.Create(Guid.Parse(command.WarehouseId));
        var roomId = RoomId.Create(Guid.Parse(command.RoomId));
        var warehouseAndRoomNamesResult = _warehouseAndRoomExistenceChecker.CheckExistenceAndGetNames(warehouseId, roomId);
        if (warehouseAndRoomNamesResult.IsError)
        {
            return warehouseAndRoomNamesResult.Errors;
        }
        var carton = cartonResult.Value;

        var result = _cartonLocationAssigner.AssignLocationToCarton(carton, warehouseId, warehouseAndRoomNamesResult.Value["WarehouseName"], roomId, warehouseAndRoomNamesResult.Value["RoomName"], command.OnLeft, command.Below, command.Behind);

        if (result.IsError)
        {
            return result.Errors;
        }
        carton = result.Value;
        _cartonRepository.Update(carton);

        return await Task.FromResult<ErrorOr<Carton>>(carton);
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