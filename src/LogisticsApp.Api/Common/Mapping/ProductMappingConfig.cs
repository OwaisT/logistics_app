using LogisticsApp.Application.Products.Commands.CreateProduct;
using LogisticsApp.Contracts.Product;
using LogisticsApp.Domain.Aggregates.Product;
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
    }
}