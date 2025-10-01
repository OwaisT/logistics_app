using LogisticsApp.Application.Orders.CreateOrder;
using LogisticsApp.Contracts.Order;
using LogisticsApp.Domain.BoundedContexts.Catalog.Aggregates.Order;
using LogisticsApp.Domain.BoundedContexts.Catalog.Aggregates.Order.ValueObjects;
using Mapster;

namespace LogisticsApp.Api.Common.Mapping;

public class OrderMappingConfig : IRegister
{
    public void Register(TypeAdapterConfig config)
    {

        config.NewConfig<CreateOrderRequest, CreateOrderCommand>()
            .Map(dest => dest, src => src);

        config.NewConfig<OrderItemRequest, OrderItemCommand>()
            .Map(dest => dest, src => src);

        config.NewConfig<Order, OrderResponse>()
            .Map(dest => dest.OrderId, src => src.Id.Value);
        
        config.NewConfig<OrderItem, OrderItemResponse>()
            .Map(dest => dest.ProductId, src => src.ProductId.Value)
            .Map(dest => dest.VariationId, src => src.VariationId.Value);
    }
}