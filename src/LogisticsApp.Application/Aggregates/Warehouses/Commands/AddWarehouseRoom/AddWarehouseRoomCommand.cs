using ErrorOr;
using LogisticsApp.Domain.BoundedContexts.Positioning.Aggregates.Warehouse;
using MediatR;

namespace LogisticsApp.Application.Warehouses.Commands.AddWarehouseRoom;

public record AddWarehouseRoomCommand(
    string WarehouseId,
    string RoomName
) : IRequest<ErrorOr<Warehouse>>;