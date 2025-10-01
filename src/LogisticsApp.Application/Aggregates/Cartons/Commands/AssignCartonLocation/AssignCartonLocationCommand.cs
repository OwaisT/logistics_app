using ErrorOr;
using LogisticsApp.Domain.BoundedContexts.Positioning.Aggregates.Carton;
using MediatR;

namespace LogisticsApp.Application.Aggregates.Cartons.Commands.AssignCartonLocation;

public record AssignCartonLocationCommand(
    string CartonId,
    string WarehouseId,
    string RoomId,
    int OnLeft,
    int Below,
    int Behind) : IRequest<ErrorOr<Carton>>;