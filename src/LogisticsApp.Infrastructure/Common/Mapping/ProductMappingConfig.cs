using LogisticsApp.Domain.BoundedContexts.Catalog.Aggregates.ProductAggregate;
using LogisticsApp.Domain.BoundedContexts.Catalog.Aggregates.ProductAggregate.Entities;
using LogisticsApp.Domain.BoundedContexts.Catalog.Aggregates.ProductAggregate.ValueObjects;
using LogisticsApp.Infrastructure.Persistence.Aggregates.Products.Entities;
using LogisticsApp.Infrastructure.Persistence.Aggregates.Products.Helpers;
using Mapster;

namespace LogisticsApp.Infrastructure.Common.Mapping;

public class ProductMappingConfig : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        // Note: older/newer Mapster versions don't expose AllowImplicitDestinationInheritance
        // The mapping registrations below should work without that setting.


        config.NewConfig<ProductEntity, Product>()
            // Use a helper that will rehydrate the aggregate via reflection so the domain remains untouched.
            .ConstructUsing(src => ProductRehydrationHelper.Rehydrate(src))
            .Map(dest => dest.Variations, src => src.Variations.Adapt<List<Variation>>())
            .Map(dest => dest.Categories, src => src.Categories.Adapt<List<string>>())
            .Map(dest => dest.Colors, src => src.Colors.Adapt<List<string>>())
            .Map(dest => dest.Sizes, src => src.Sizes.Adapt<List<string>>());

        // Map primitive Guid to domain Id value objects
        config.NewConfig<Guid, ProductId>()
            .ConstructUsing(g => ProductId.Create(g));

        config.NewConfig<Guid, VariationId>()
            .ConstructUsing(g => VariationId.Create(g));

        config.NewConfig<VariationEntity, Variation>()
            .ConstructUsing(src => VariationRehydrationHelper.Rehydrate(src))
            .Map(dest => dest.Id, src => VariationId.Create(src.Id));

        config.NewConfig<CategoryEntity, string>()
            .Map(dest => dest, src => src.Name);

        config.NewConfig<ColorEntity, string>()
            .Map(dest => dest, src => src.Name);
        
        config.NewConfig<SizeEntity, string>()
            .Map(dest => dest, src => src.Name);
    }
}