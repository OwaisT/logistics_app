using ErrorOr;
using LogisticsApp.Domain.BoundedContexts.Positioning.Aggregates.Carton;
using MediatR;

namespace LogisticsApp.Application.Aggregates.Cartons.Commands.CreateCarton;

public record CreateCartonCommand() : IRequest<ErrorOr<Carton>>;