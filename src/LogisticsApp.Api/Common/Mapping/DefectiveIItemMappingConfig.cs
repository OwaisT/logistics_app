using LogisticsApp.Application.Aggregates.DefectiveItems.Commands.CreateDefectiveItem;
using LogisticsApp.Application.Aggregates.DefectiveItems.Commands.MarkItemAsRepaired;
using LogisticsApp.Contracts.Aggregates.DefectiveItem;
using LogisticsApp.Domain.BoundedContexts.Catalog.Aggregates.DefectiveItemAggregate;
using Mapster;

namespace LogisticsApp.Api.Common.Mapping;

public class DefectiveItemMappingConfig : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<CreateDefectiveItemRequest, CreateDefectiveItemCommand>();
        config.NewConfig<DefectiveItem, DefectiveItemResponse>()
            .Map(dest => dest.Id, src => src.Id.Value)
            .Map(dest => dest.ProductId, src => src.ProductId.Value)
            .Map(dest => dest.VariationId, src => src.VariationId.Value)
            .Map(dest => dest, src => src);

        config.NewConfig<string, MarkItemAsRepairedCommand>()
            .Map(dest => dest.DefectiveItemId, src => src);
    }
}
