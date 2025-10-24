using ErrorOr;
using LogisticsApp.Domain.BoundedContexts.Catalog.Aggregates.DefectiveItemAggregate;
using MediatR;

namespace LogisticsApp.Application.Aggregates.DefectiveItems.Commands.CreateDefectiveItem;

public record CreateDefectiveItemCommand(
    string ProductId,
    string VariationId,
    string Reason,
    bool IsRepairable
) : IRequest<ErrorOr<DefectiveItem>>;