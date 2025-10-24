using LogisticsApp.Application.Aggregates.DefectiveItems.Commands.CreateDefectiveItem;
using LogisticsApp.Application.Aggregates.DefectiveItems.Commands.MarkItemAsRepaired;
using LogisticsApp.Application.Aggregates.Orders.Commands.CreateOrder;
using LogisticsApp.Application.Aggregates.Orders.Commands.UpdateOrderItemsStatus;
using LogisticsApp.Application.Aggregates.Orders.Commands.UpdateOrderStatus;
using LogisticsApp.Contracts.Aggregates.DefectiveItem;
using LogisticsApp.Contracts.Aggregates.Order;
using MapsterMapper;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LogisticsApp.Api.Controllers.AggregatesControllers;

[Route("/DefectiveItems")]
public class DefectiveItemsController(ISender _mediator, IMapper _mapper) : ApiController
{

    [Authorize(Roles = "BusinessManager,FacilityManager,FacilityWorker")]
    [HttpPost]
    public async Task<IActionResult> CreateDefectiveItem(CreateDefectiveItemRequest request)
    {
        var command = _mapper.Map<CreateDefectiveItemCommand>(request);
        var createDefectiveItemResult = await _mediator.Send(command);
        return createDefectiveItemResult.Match(
            defectiveItem => Ok(_mapper.Map<DefectiveItemResponse>(defectiveItem)),
            Problem);
    }

    [Authorize(Roles = "BusinessManager,FacilityManager,FacilityWorker")]
    [HttpPut("{defectiveItemId}/Repaired")]
    public async Task<IActionResult> MarkItemAsRepaired(string defectiveItemId)
    {
        var command = new MarkItemAsRepairedCommand(defectiveItemId);
        var markItemAsRepairedResult = await _mediator.Send(command);
        return markItemAsRepairedResult.Match(
            defectiveItem => Ok(_mapper.Map<DefectiveItemResponse>(defectiveItem)),
            Problem);
    }
}
