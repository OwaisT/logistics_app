using LogisticsApp.Application.Aggregates.Products.Commands.AddProductColor;
using LogisticsApp.Application.Aggregates.Products.Commands.AddReceivedForVariation;
using LogisticsApp.Application.Aggregates.Products.Commands.CreateProduct;
using LogisticsApp.Contracts.Aggregates.Product;
using LogisticsApp.Contracts.Aggregates.Product.Requests.Modifications;
using LogisticsApp.Domain.BoundedContexts.Catalog.Aggregates.ProductAggregate;
using LogisticsApp.Domain.BoundedContexts.Catalog.Aggregates.ProductAggregate.Entities;
using LogisticsApp.Domain.BoundedContexts.Catalog.Aggregates.ProductAggregate.ValueObjects;
using Mapster;

namespace LogisticsApp.Api.Common.Mapping;

public class ProductMappingConfig : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<CreateProductRequest, CreateProductCommand>()
            .Map(dest => dest, src => src);

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

        config.NewConfig<(string productId, AddReceivedForVariationRequest request), AddReceivedForVariationCommand>()
            .Map(dest => dest.ProductId, src => src.productId)
            .Map(dest => dest, src => src.request); 

        config.NewConfig<(string productId, AddProductColorsRequest request), AddProductColorsCommand>()
            .Map(dest => dest.ProductId, src => src.productId)
            .Map(dest => dest, src => src.request);

    }
}