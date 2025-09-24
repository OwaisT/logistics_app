using ErrorOr;
using LogisticsApp.Application.Common.Interfaces.Persistence;
using LogisticsApp.Domain.BoundedContexts.Positioning.Aggregates.Carton;
using LogisticsApp.Domain.BoundedContexts.Positioning.Aggregates.Carton.ValueObjects;
using LogisticsApp.Domain.BoundedContexts.Positioning.Aggregates.Warehouse.ValueObjects;
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
        var cartonId = Guid.Parse(command.CartonId);
        var carton = _cartonRepository.GetById(CartonId.Create(cartonId));
        if (carton is null)
        {
            return Error.NotFound(description: "Carton not found.");
        }
        var warehouseId = WarehouseId.Create(Guid.Parse(command.WarehouseId));
        var roomId = RoomId.Create(Guid.Parse(command.RoomId));
        var warehouse = _warehouseRepository.GetById(warehouseId.Value);
        if (warehouse is null)
        {
            return Error.NotFound(description: "Warehouse not found.");
        }
        var room = warehouse.Rooms.FirstOrDefault(r => r.Id == roomId);
        if (room is null)
        {
            return Error.NotFound(description: "Room not found in warehouse.");
        }
        var warehouseName = warehouse.Name;
        var roomName = room.Name;

        carton.SetLocation(warehouseId, warehouseName, roomId, roomName, command.OnLeft, command.Below, command.Behind);

        return await Task.FromResult<ErrorOr<Carton>>(carton);
    }
}