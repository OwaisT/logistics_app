using ErrorOr;
using LogisticsApp.Domain.BoundedContexts.Positioning.Aggregates.Warehouse;
using MediatR;

namespace LogisticsApp.Application.Aggregates.Warehouses.Queries.GetWarehouse;

public record GetWarehouseQuery(string WarehouseId) : IRequest<ErrorOr<Warehouse>>;