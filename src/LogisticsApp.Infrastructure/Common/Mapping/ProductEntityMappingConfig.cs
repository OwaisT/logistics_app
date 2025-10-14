using LogisticsApp.Domain.BoundedContexts.Catalog.Aggregates.ProductAggregate;
using LogisticsApp.Domain.BoundedContexts.Catalog.Aggregates.ProductAggregate.Entities;
using LogisticsApp.Infrastructure.Persistence.Aggregates.Products.Entities;
using Mapster;

namespace LogisticsApp.Infrastructure.Common.Mapping;

public class ProductEntityMappingConfig : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<Product, ProductEntity>()
            .Map(dest => dest.Id, src => src.Id.Value)
            .Map(dest => dest.Variations, src => src.Variations.Adapt<List<VariationEntity>>())
            .Ignore(dest => dest.Categories)
            .Ignore(dest => dest.Colors)
            .Ignore(dest => dest.Sizes);
        
        config.NewConfig<Variation, VariationEntity>()
            .Map(dest => dest.Id, src => src.Id.Value);
    }
}