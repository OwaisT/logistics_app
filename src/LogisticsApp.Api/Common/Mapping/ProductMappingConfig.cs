using LogisticsApp.Application.Aggregates.Products.Commands.CreateProduct;
using LogisticsApp.Application.Aggregates.Products.Commands.Modifications.Category.AddProductCategories;
using LogisticsApp.Application.Aggregates.Products.Commands.Modifications.Category.RemoveProductCategories;
using LogisticsApp.Application.Aggregates.Products.Commands.Modifications.Color.AddProductColors;
using LogisticsApp.Application.Aggregates.Products.Commands.Modifications.Color.RemoveProductColors;
using LogisticsApp.Application.Aggregates.Products.Commands.Modifications.ModifyProductAssortments;
using LogisticsApp.Application.Aggregates.Products.Commands.Modifications.ModifyProductGeneralPrice;
using LogisticsApp.Application.Aggregates.Products.Commands.Modifications.ModifyProductRefCode;
using LogisticsApp.Application.Aggregates.Products.Commands.Modifications.ModifyProductSeason;
using LogisticsApp.Application.Aggregates.Products.Commands.Modifications.Size.AddProductSizes;
using LogisticsApp.Application.Aggregates.Products.Commands.Modifications.Size.RemoveProductSizes;
using LogisticsApp.Application.Aggregates.Products.Commands.Modifications.Variations.AddProductVariations;
using LogisticsApp.Application.Aggregates.Products.Commands.Modifications.Variations.AddReceivedForVariation;
using LogisticsApp.Application.Aggregates.Products.Commands.Modifications.Variations.ModifyVariationsPrice;
using LogisticsApp.Application.Aggregates.Products.Commands.Modifications.Variations.RemoveProductVariations;
using LogisticsApp.Contracts.Aggregates.Product;
using LogisticsApp.Contracts.Aggregates.Product.Requests;
using LogisticsApp.Contracts.Aggregates.Product.Requests.Modifications;
using LogisticsApp.Contracts.Aggregates.Product.Requests.Modifications.Category;
using LogisticsApp.Contracts.Aggregates.Product.Requests.Modifications.Color;
using LogisticsApp.Contracts.Aggregates.Product.Requests.Modifications.Size;
using LogisticsApp.Contracts.Aggregates.Product.Requests.Modifications.Variations;
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

        
        ConfigureProductModificationMappings(config);

    }
    
    private static void ConfigureProductModificationMappings(TypeAdapterConfig config)
    {
        config.NewConfig<(string productId, AddProductColorsRequest request), AddProductColorsCommand>()
            .Map(dest => dest.ProductId, src => src.productId)
            .Map(dest => dest, src => src.request);

        config.NewConfig<(string productId, RemoveProductColorsRequest request), RemoveProductColorsCommand>()
            .Map(dest => dest.ProductId, src => src.productId)
            .Map(dest => dest, src => src.request);

        config.NewConfig<(string productId, AddProductCategoriesRequest request), AddProductCategoriesCommand>()
            .Map(dest => dest.ProductId, src => src.productId)
            .Map(dest => dest, src => src.request);

        config.NewConfig<(string productId, RemoveProductCategoriesRequest request), RemoveProductCategoriesCommand>()
            .Map(dest => dest.ProductId, src => src.productId)
            .Map(dest => dest, src => src.request);

        config.NewConfig<(string productId, ModifyProductAssortmentsRequest request), ModifyProductAssortmentsCommand>()
            .Map(dest => dest.ProductId, src => src.productId)
            .Map(dest => dest, src => src.request);

        config.NewConfig<(string productId, ModifyProductGeneralPriceRequest request), ModifyProductGeneralPriceCommand>()
            .Map(dest => dest.ProductId, src => src.productId)
            .Map(dest => dest, src => src.request);

        config.NewConfig<(string productId, ModifyProductRefCodeRequest request), ModifyProductRefCodeCommand>()
            .Map(dest => dest.ProductId, src => src.productId)
            .Map(dest => dest, src => src.request);

        config.NewConfig<(string productId, ModifyProductSeasonRequest request), ModifyProductSeasonCommand>()
            .Map(dest => dest.ProductId, src => src.productId)
            .Map(dest => dest, src => src.request);

        config.NewConfig<(string productId, AddProductSizesRequest request), AddProductSizesCommand>()
            .Map(dest => dest.ProductId, src => src.productId)
            .Map(dest => dest, src => src.request);

        config.NewConfig<(string productId, RemoveProductSizesRequest request), RemoveProductSizesCommand>()
            .Map(dest => dest.ProductId, src => src.productId)
            .Map(dest => dest, src => src.request);

        config.NewConfig<(string productId, AddProductVariationsRequest request), AddProductVariationsCommand>()
            .Map(dest => dest.ProductId, src => src.productId)
            .Map(dest => dest, src => src.request);

        config.NewConfig<(string productId, RemoveProductVariationsRequest request), RemoveProductVariationsCommand>()
            .Map(dest => dest.ProductId, src => src.productId)
            .Map(dest => dest, src => src.request);

        config.NewConfig<(string productId, AddReceivedForVariationRequest request), AddReceivedForVariationCommand>()
            .Map(dest => dest.ProductId, src => src.productId)
            .Map(dest => dest, src => src.request);

        config.NewConfig<(string productId, ModifyVariationsPriceRequest request), ModifyVariationsPriceCommand>()
            .Map(dest => dest.ProductId, src => src.productId)
            .Map(dest => dest, src => src.request);

        // Additional mapping configurations for product modifications can be added here in the future.
    }
}