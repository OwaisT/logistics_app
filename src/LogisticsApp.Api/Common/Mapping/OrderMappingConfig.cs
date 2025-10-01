using LogisticsApp.Application.Aggregates.Orders.Commands.CreateOrder;
using LogisticsApp.Application.Aggregates.Orders.Commands.UpdateOrderStatus;
using LogisticsApp.Contracts.Aggregates.Order;
using LogisticsApp.Domain.BoundedContexts.Catalog.Aggregates.Order;
using LogisticsApp.Domain.BoundedContexts.Catalog.Aggregates.Order.Entities;
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

        config.NewConfig<(Guid orderId, UpdateOrderStatusRequest request), UpdateOrderStatusCommand>()
            .Map(dest => dest.OrderId, src => src.orderId)
            .Map(dest => dest, src => src.request);

        config.NewConfig<Order, OrderResponse>()
            .Map(dest => dest.OrderId, src => src.Id.Value);

        config.NewConfig<OrderItem, OrderItemResponse>()
            .Map(dest => dest.Id, src => src.Id.Value)
            .Map(dest => dest.ProductId, src => src.ProductId.Value)
            .Map(dest => dest.VariationId, src => src.VariationId.Value);
    }
}