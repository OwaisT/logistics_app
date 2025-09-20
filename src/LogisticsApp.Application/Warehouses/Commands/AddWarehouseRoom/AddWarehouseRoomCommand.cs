using ErrorOr;
using LogisticsApp.Domain.Aggregates.Warehouse;
using MediatR;

namespace LogisticsApp.Application.Warehouses.Commands.AddWarehouseRoom;

public record AddWarehouseRoomCommand(
    string WarehouseId,
    string RoomName
) : IRequest<ErrorOr<Warehouse>>;