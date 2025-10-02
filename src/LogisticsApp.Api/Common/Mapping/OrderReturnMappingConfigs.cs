using LogisticsApp.Application.Aggregates.OrderReturns.Commands.CreateOrderReturn;
using LogisticsApp.Application.Aggregates.OrderReturns.Commands.UpdateOrderReturnStatus;
using LogisticsApp.Contracts.Aggregates.OrderReturn;
using LogisticsApp.Domain.BoundedContexts.Catalog.Aggregates.OrderReturnAggregate;
using LogisticsApp.Domain.BoundedContexts.Catalog.Aggregates.OrderReturnAggregate.Entities;
using Mapster;

namespace LogisticsApp.Api.Common.Mapping;

public class OrderReturnMappingConfig : IRegister
{
    public void Register(TypeAdapterConfig config)
    {

        config.NewConfig<CreateOrderReturnRequest, CreateOrderReturnCommand>()
            .Map(dest => dest, src => src);

        config.NewConfig<(Guid orderReturnId, UpdateOrderReturnStatusRequest request), UpdateOrderReturnStatusCommand>()
            .Map(dest => dest.OrderReturnId, src => src.orderReturnId)
            .Map(dest => dest, src => src.request);

        config.NewConfig<OrderReturn, OrderReturnResponse>()
            .Map(dest => dest.Id, src => src.Id.Value)
            .Map(dest => dest.OrderId, src => src.OrderId.Value);

        config.NewConfig<OrderReturnItem, OrderReturnItemResponse>()
            .Map(dest => dest.Id, src => src.Id.Value)
            .Map(dest => dest.OrderItemId, src => src.OrderItemId.Value)
            .Map(dest => dest.ProductId, src => src.ProductId.Value)
            .Map(dest => dest.VariationId, src => src.VariationId.Value);
    }
}