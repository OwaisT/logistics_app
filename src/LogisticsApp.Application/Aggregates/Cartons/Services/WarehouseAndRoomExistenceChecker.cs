using ErrorOr;
using LogisticsApp.Application.Common.Interfaces.Persistence;
using LogisticsApp.Application.Common.Interfaces.Services;
using LogisticsApp.Domain.BoundedContexts.Positioning.Aggregates.Warehouse.ValueObjects;
using LogisticsApp.Domain.Common.Errors;

namespace LogisticsApp.Application.Aggregates.Cartons.Services;

public class WarehouseAndRoomExistenceChecker(
    IWarehouseRepository _warehouseRepository) : IWarehouseAndRoomExistenceChecker 
{
    public ErrorOr<Dictionary<string, string>> CheckExistenceAndGetNames(WarehouseId warehouseId, RoomId roomId)
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