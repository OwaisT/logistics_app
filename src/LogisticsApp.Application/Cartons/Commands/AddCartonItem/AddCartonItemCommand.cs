using ErrorOr;
using LogisticsApp.Domain.BoundedContexts.Positioning.Aggregates.Carton;
using MediatR;

namespace LogisticsApp.Application.Cartons.Commands.AddCartonItem;

public record AddCartonItemCommand(
    string CartonId,
    string ProductId,
    string VariationId,
    string RefCode,
    int Quantity
) : IRequest<ErrorOr<Carton>>;