
using LogisticsApp.Application.Cartons.Commands.CreateCarton;
using LogisticsApp.Contracts.Carton;
using LogisticsApp.Domain.Aggregates.Carton;
using LogisticsApp.Domain.Aggregates.Carton.ValueObjects;
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

    }
}