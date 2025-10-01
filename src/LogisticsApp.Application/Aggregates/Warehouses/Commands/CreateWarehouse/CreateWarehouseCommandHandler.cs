using ErrorOr;
using LogisticsApp.Application.Aggregates.Warehouses.Services;
using LogisticsApp.Application.Common.Interfaces.Persistence;
using LogisticsApp.Domain.BoundedContexts.Positioning.Aggregates.Warehouse;
using MediatR;

namespace LogisticsApp.Application.Aggregates.Warehouses.Commands.CreateWarehouse;

public class CreateWarehouseCommandHandler :
    IRequestHandler<CreateWarehouseCommand, ErrorOr<Warehouse>>
{
    private readonly IWarehouseRepository _warehouseRepository;

    public CreateWarehouseCommandHandler(IWarehouseRepository warehouseRepository)
    {
        _warehouseRepository = warehouseRepository;
    }
    public async Task<ErrorOr<Warehouse>> Handle(CreateWarehouseCommand command, CancellationToken cancellationToken)
    {
        await Task.CompletedTask;
        var warehouse = Warehouse.Create(
            command.Name,
            command.Street,
            command.Area,
            command.City,
            command.Postcode,
            command.Country,
            new WarehouseUniquenessChecker(_warehouseRepository)
            );
        
        if (warehouse.IsError)
        {
            return warehouse.Errors;
        }

        _warehouseRepository.Add(warehouse.Value);

        return await Task.FromResult<ErrorOr<Warehouse>>(warehouse.Value);
    }
}