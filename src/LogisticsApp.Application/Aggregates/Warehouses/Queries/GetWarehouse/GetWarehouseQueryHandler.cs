using ErrorOr;
using LogisticsApp.Application.Common.Interfaces.Persistence;
using LogisticsApp.Domain.BoundedContexts.Positioning.Aggregates.Warehouse;
using LogisticsApp.Domain.BoundedContexts.Positioning.Aggregates.Warehouse.ValueObjects;
using LogisticsApp.Domain.Common.Errors;
using MediatR;

namespace LogisticsApp.Application.Aggregates.Warehouses.Queries.GetWarehouse;

public class GetWarehouseQueryHandler :
    IRequestHandler<GetWarehouseQuery, ErrorOr<Warehouse>>
{
    private readonly IWarehouseRepository _warehouseRepository;

    public GetWarehouseQueryHandler(IWarehouseRepository warehouseRepository)
    {
        _warehouseRepository = warehouseRepository;
    }
    public async Task<ErrorOr<Warehouse>> Handle(GetWarehouseQuery query, CancellationToken cancellationToken)
    {
        await Task.CompletedTask;
        var warehouseId = WarehouseId.Create(Guid.Parse(query.WarehouseId));
        var warehouse = _warehouseRepository.GetById(warehouseId);

        if (warehouse is null)
        {
            return Errors.Common.EntityNotFound(nameof(Warehouse), query.WarehouseId);
        }

        return warehouse;
    }
}