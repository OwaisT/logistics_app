using ErrorOr;
using LogisticsApp.Application.Common.Interfaces.Persistence;
using LogisticsApp.Domain.Aggregates.Warehouse;
using MediatR;

namespace LogisticsApp.Application.Warehouses.Commands.AddWarehouseRoom;

public class AddWarehouseRoomCommandHandler :
    IRequestHandler<AddWarehouseRoomCommand, ErrorOr<Warehouse>>
{
    private readonly IWarehouseRepository _warehouseRepository;

    public AddWarehouseRoomCommandHandler(IWarehouseRepository warehouseRepository)
    {
        _warehouseRepository = warehouseRepository;
    }
    public async Task<ErrorOr<Warehouse>> Handle(AddWarehouseRoomCommand command, CancellationToken cancellationToken)
    {
        await Task.CompletedTask;
        var warehouseId = Guid.Parse(command.WarehouseId);
        var warehouse = _warehouseRepository.GetById(warehouseId);
        if (warehouse is null)
        {
            return Error.NotFound(description: "Warehouse not found.");
        }
        warehouse.AddWarehouseRoom(command.RoomName);

        return await Task.FromResult<ErrorOr<Warehouse>>(warehouse);
    }
}