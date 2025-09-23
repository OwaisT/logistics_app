using ErrorOr;
using LogisticsApp.Domain.BoundedContexts.Positioning.Aggregates.Carton;
using MediatR;

namespace LogisticsApp.Application.Cartons.Commands.RemoveCartonItem;

public record RemoveCartonItemCommand(
    string CartonId,
    string ProductId,
    string VariationId,
    int Quantity
) : IRequest<ErrorOr<Carton>>;