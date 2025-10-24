using ErrorOr;
using LogisticsApp.Application.Aggregates.DefectiveItems.Commands.CreateDefectiveItem;
using LogisticsApp.Application.Common.Interfaces.Persistence;
using LogisticsApp.Domain.BoundedContexts.Catalog.Aggregates.DefectiveItemAggregate;
using LogisticsApp.Domain.BoundedContexts.Catalog.Aggregates.DefectiveItemAggregate.ValueObjects;
using LogisticsApp.Domain.BoundedContexts.Catalog.Aggregates.ProductAggregate;
using LogisticsApp.Domain.BoundedContexts.Catalog.Aggregates.ProductAggregate.ValueObjects;
using LogisticsApp.Domain.Common.Errors;
using MediatR;

namespace LogisticsApp.Application.Aggregates.DefectiveItems.Commands.MarkItemAsRepaired;

public class MarkItemAsRepairedCommandHandler(
    IDefectiveItemRepository _defectiveItemRepository)
     : IRequestHandler<MarkItemAsRepairedCommand, ErrorOr<DefectiveItem>>
{
    public async Task<ErrorOr<DefectiveItem>> Handle(MarkItemAsRepairedCommand command, CancellationToken cancellationToken)
    {
        await Task.CompletedTask;
        var defectiveItemId = DefectiveItemId.Create(Guid.Parse(command.DefectiveItemId));
        var defectiveItem = _defectiveItemRepository.GetById(defectiveItemId);
        if (defectiveItem is null)
        {
            return Errors.Common.EntityNotFound("DefectiveItem", command.DefectiveItemId);
        }
        var defectiveItemResult = defectiveItem.MarkAsRepaired();
        if (defectiveItemResult.IsError)
        {
            return defectiveItemResult.Errors;
        }
        _defectiveItemRepository.Update(defectiveItemResult.Value);

        return await Task.FromResult<ErrorOr<DefectiveItem>>(defectiveItemResult.Value);
    }
}
