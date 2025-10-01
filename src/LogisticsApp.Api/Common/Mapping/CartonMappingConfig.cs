
using LogisticsApp.Application.Aggregates.Cartons.Commands.AddCartonItem;
using LogisticsApp.Application.Aggregates.Cartons.Commands.AssignCartonLocation;
using LogisticsApp.Application.Aggregates.Cartons.Commands.CreateCarton;
using LogisticsApp.Application.Aggregates.Cartons.Commands.RemoveCartonItem;
using LogisticsApp.Contracts.Aggregates.Carton;
using LogisticsApp.Domain.BoundedContexts.Positioning.Aggregates.Carton;
using LogisticsApp.Domain.BoundedContexts.Positioning.Aggregates.Carton.ValueObjects;
using Mapster;

namespace LogisticsApp.Api.Common.Mapping;

public class CartonMappingConfig : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<CreateCartonRequest, CreateCartonCommand>()
            .Map(dest => dest, src => src);

        config.NewConfig<Carton, CartonResponse>()
            .Map(dest => dest.CartonId, src => src.Id.Value)
            .Map(dest => dest, src => src);

        config.NewConfig<CartonLocation, CartonLocationResponse>()
            .Map(dest => dest.WarehouseId, src => src.WarehouseId.Value)
            .Map(dest => dest.RoomId, src => src.RoomId.Value)
            .Map(dest => dest, src => src);

        config.NewConfig<CartonItem, CartonItemResponse>()
            .Map(dest => dest.ProductId, src => src.ProductId.Value)
            .Map(dest => dest.VariationId, src => src.VariationId.Value)
            .Map(dest => dest, src => src);

        config.NewConfig<(AddCartonItemRequest request, string cartonId), AddCartonItemCommand>()
            .Map(dest => dest.CartonId, src => src.cartonId)
            .Map(dest => dest, src => src.request);

        config.NewConfig<(RemoveCartonItemRequest request, string cartonId), RemoveCartonItemCommand>()
            .Map(dest => dest.CartonId, src => src.cartonId)
            .Map(dest => dest, src => src.request);
        
        config.NewConfig<(AssignCartonLocationRequest request, string cartonId), AssignCartonLocationCommand>()
            .Map(dest => dest.CartonId, src => src.cartonId)
            .Map(dest => dest, src => src.request);
    }
}