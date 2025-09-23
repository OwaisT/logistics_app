using ErrorOr;
using LogisticsApp.Domain.BoundedContexts.Positioning.Aggregates.Warehouse;
using MediatR;

namespace LogisticsApp.Application.Warehouses.Commands.CreateWarehouse;

public record CreateWarehouseCommand(
    string Name,
    string Street,
    string Area,
    string City,
    string Postcode,
    string Country
) : IRequest<ErrorOr<Warehouse>>;