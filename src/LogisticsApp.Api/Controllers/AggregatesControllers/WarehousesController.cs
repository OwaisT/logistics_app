using LogisticsApp.Application.Aggregates.Warehouses.Commands.CreateWarehouse;
using LogisticsApp.Application.Aggregates.Warehouses.Queries.GetWarehouse;
using LogisticsApp.Application.Warehouses.Commands.AddWarehouseRoom;
using LogisticsApp.Contracts.Aggregates.Warehouse;
using MapsterMapper;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LogisticsApp.Api.Controllers.AggregatesControllers;

[Route("/Warehouse")]
public class WarehousesController : ApiController
{
    private readonly ISender _mediator;

    private readonly IMapper _mapper;

    public WarehousesController(ISender mediator, IMapper mapper)
    {
        _mediator = mediator;
        _mapper = mapper;
    }

    [Authorize(Roles = "BusinessManager")]
    [HttpPost]
    public async Task<IActionResult> CreateWarehouse(CreateWarehouseRequest request)
    {
        var command = _mapper.Map<CreateWarehouseCommand>(request);
        var createWarehouseResult = await _mediator.Send(command);
        return createWarehouseResult.Match(
            warehouse => Ok(_mapper.Map<WarehouseResponse>(warehouse)),
            Problem);
    }

    [Authorize(Roles = "BusinessManager,FacilityManager")]
    [HttpGet("{warehouseId}")]
    public async Task<IActionResult> GetWarehouse(string warehouseId)
    {
        var query = new GetWarehouseQuery(warehouseId);
        var warehouse = await _mediator.Send(query);
        return warehouse.Match(
            warehouse => Ok(_mapper.Map<WarehouseResponse>(warehouse)),
            Problem);
    }

    [Authorize(Roles = "BusinessManager,FacilityManager")]
    [HttpPost("{warehouseId}/rooms")]
    public async Task<IActionResult> AddRoomToWarehouse(AddWarehouseRoomRequest request, string warehouseId)
    {
        var command = _mapper.Map<AddWarehouseRoomCommand>((request, warehouseId));
        var addRoomResult = await _mediator.Send(command);
        return addRoomResult.Match(
            room => Ok(_mapper.Map<WarehouseResponse>(room)),
            Problem);
    }

}
