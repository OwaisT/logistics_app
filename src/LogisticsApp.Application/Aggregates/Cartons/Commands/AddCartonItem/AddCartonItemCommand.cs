using ErrorOr;
using LogisticsApp.Domain.BoundedContexts.Positioning.Aggregates.Carton;
using MediatR;

namespace LogisticsApp.Application.Aggregates.Cartons.Commands.AddCartonItem;

public record AddCartonItemCommand(
    string CartonId,
    string ProductId,
    string VariationId,
    int Quantity
) : IRequest<ErrorOr<Carton>>;