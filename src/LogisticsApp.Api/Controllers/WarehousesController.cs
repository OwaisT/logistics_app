using LogisticsApp.Application.Warehouses.Commands.AddWarehouseRoom;
using LogisticsApp.Application.Warehouses.Commands.CreateWarehouse;
using LogisticsApp.Contracts.Warehouse;
using MapsterMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace LogisticsApp.Api.Controllers;

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

    [HttpPost]
    public async Task<IActionResult> CreateWarehouse(CreateWarehouseRequest request)
    {
        var command = _mapper.Map<CreateWarehouseCommand>(request);
        var createWarehouseResult = await _mediator.Send(command);
        return createWarehouseResult.Match(
            warehouse => Ok(_mapper.Map<WarehouseResponse>(warehouse)),
            Problem);
    }

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
