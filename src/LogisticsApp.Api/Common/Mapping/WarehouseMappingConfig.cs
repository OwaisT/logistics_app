using LogisticsApp.Application.Warehouses.Commands.AddWarehouseRoom;
using LogisticsApp.Application.Warehouses.Commands.CreateWarehouse;
using LogisticsApp.Contracts.Warehouse;
using LogisticsApp.Domain.BoundedContexts.Positioning.Aggregates.Warehouse;
using LogisticsApp.Domain.BoundedContexts.Positioning.Aggregates.Warehouse.Entities;
using Mapster;

namespace LogisticsApp.Api.Common.Mapping;

public class WarehouseMappingConfig : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<CreateWarehouseRequest, CreateWarehouseCommand>()
            .Map(dest => dest, src => src);

        config.NewConfig<Warehouse, WarehouseResponse>()
            .Map(dest => dest.WarehouseId, src => src.Id.Value)
            .Map(dest => dest, src => src);

        config.NewConfig<Room, RoomResponse>()
            .Map(dest => dest.Id, src => src.Id.Value)
            .Map(dest => dest, src => src);

        config.NewConfig<(AddWarehouseRoomRequest request, string warehouseId), AddWarehouseRoomCommand>()
            .Map(dest => dest.WarehouseId, src => src.warehouseId)
            .Map(dest => dest, src => src.request);

    }
}
