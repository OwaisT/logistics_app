using ErrorOr;
using LogisticsApp.Application.Common.Interfaces.Persistence;
using LogisticsApp.Domain.BoundedContexts.Positioning.Aggregates.Carton;
using LogisticsApp.Domain.BoundedContexts.Positioning.Aggregates.Carton.ValueObjects;
using LogisticsApp.Domain.BoundedContexts.Positioning.Aggregates.Warehouse.ValueObjects;
using LogisticsApp.Domain.Common.Errors;
using MediatR;

namespace LogisticsApp.Application.Cartons.Commands.AssignCartonLocation;

public class AssignCartonLocationCommandHandler :
    IRequestHandler<AssignCartonLocationCommand, ErrorOr<Carton>>
{
    private readonly ICartonRepository _cartonRepository;
    private readonly IWarehouseRepository _warehouseRepository;

    public AssignCartonLocationCommandHandler(ICartonRepository cartonRepository, IWarehouseRepository warehouseRepository)
    {
        _cartonRepository = cartonRepository;
        _warehouseRepository = warehouseRepository;
    }
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
        var warehouseAndRoomNamesResult = EnforceWarehouseInvariantsAndGetWarehouseAndRoomNames(
            warehouseId,
            roomId);
        if (warehouseAndRoomNamesResult.IsError)
        {
            return warehouseAndRoomNamesResult.Errors;
        }
        var carton = cartonResult.Value;
        var warehouseAndRoomNames = warehouseAndRoomNamesResult.Value;

        carton.SetLocation(warehouseId, warehouseAndRoomNames["WarehouseName"], roomId, warehouseAndRoomNames["RoomName"], command.OnLeft, command.Below, command.Behind);

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

    private ErrorOr<Dictionary<string, string>> EnforceWarehouseInvariantsAndGetWarehouseAndRoomNames(WarehouseId warehouseId, RoomId roomId)
    {
        if (warehouseId == null || roomId == null)
        {
            return Errors.Common.InvalidInput("Invalid warehouse or room information.");
        }
        var warehouse = _warehouseRepository.GetById(warehouseId);
        if (warehouse is null)
        {
            return Errors.Common.EntityNotFound(nameof(warehouse), warehouseId.Value.ToString());
        }
        var room = warehouse.Rooms.FirstOrDefault(r => r.Id == roomId);
        if (room is null)
        {
            return Errors.Common.EntityNotFound(nameof(warehouse) + " Room", roomId.Value.ToString());
        }
        var names = new Dictionary<string, string> { { "WarehouseName", warehouse.Name }, { "RoomName", room.Name } };
        return names;
    }

}