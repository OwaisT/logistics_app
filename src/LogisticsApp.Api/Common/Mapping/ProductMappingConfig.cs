using LogisticsApp.Application.Products.Commands.CreateProduct;
using LogisticsApp.Application.Warehouses.Commands.CreateWarehouse;
using LogisticsApp.Contracts.Product;
using LogisticsApp.Contracts.Warehouse;
using LogisticsApp.Domain.Aggregates.Product;
using LogisticsApp.Domain.Aggregates.Product.Entities;
using LogisticsApp.Domain.Aggregates.Warehouse;
using Mapster;

namespace LogisticsApp.Api.Common.Mapping;

public class ProductMappingConfig : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<(CreateProductRequest request, string hostId), CreateProductCommand>()
            .Map(dest => dest, src => src.request);

        config.NewConfig<Product, ProductResponse>()
            .Map(dest => dest.Categories, src => src.Categories.ToList())
            .Map(dest => dest.Colors, src => src.Colors.ToList())
            .Map(dest => dest.Sizes, src => src.Sizes.ToList())
            .Map(dest => dest.ProductId, src => src.Id.Value);

        config.NewConfig<Assortment, AssortmentResponse>()
            .Map(dest => dest.Color, src => src.Color)
            .Map(dest => dest.Sizes, src => src.Sizes.ToDictionary(k => k.Key, v => v.Value));

        config.NewConfig<Variation, VariationResponse>()
            .Map(dest => dest.VariationId, src => src.Id.Value)
            .Map(dest => dest, src => src);

    }
}