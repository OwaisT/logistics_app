using ErrorOr;
using LogisticsApp.Domain.BoundedContexts.Catalog.Aggregates.DefectiveItemAggregate;
using MediatR;

namespace LogisticsApp.Application.Aggregates.DefectiveItems.Commands.MarkItemAsRepaired;

public record MarkItemAsRepairedCommand(
    string DefectiveItemId
) : IRequest<ErrorOr<DefectiveItem>>;