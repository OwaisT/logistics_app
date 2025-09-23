using ErrorOr;
using LogisticsApp.Domain.BoundedContexts.Positioning.Aggregates.Carton;
using MediatR;

namespace LogisticsApp.Application.Cartons.Commands.CreateCarton;

public record CreateCartonCommand() : IRequest<ErrorOr<Carton>>;