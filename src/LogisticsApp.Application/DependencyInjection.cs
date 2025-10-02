using System.Reflection;
using ErrorOr;
using FluentValidation;
using LogisticsApp.Application.Aggregates.Cartons.Services;
using LogisticsApp.Application.Aggregates.OrderReturns.Services;
using LogisticsApp.Application.Aggregates.Products.Services;
using LogisticsApp.Application.Aggregates.Warehouses.Services;
using LogisticsApp.Application.Authentication.Services;
using LogisticsApp.Application.Common.Behaviors;
using LogisticsApp.Application.Common.Services;
using LogisticsApp.Domain.BoundedContexts.Catalog.Aggregates.OrderAggregate.Services;
using LogisticsApp.Domain.BoundedContexts.Catalog.Aggregates.OrderReturnAggregate.Services;
using LogisticsApp.Domain.BoundedContexts.Catalog.Aggregates.ProductAggregate;
using LogisticsApp.Domain.BoundedContexts.Catalog.Aggregates.ProductAggregate.Services;
using LogisticsApp.Domain.BoundedContexts.Catalog.Services;
using LogisticsApp.Domain.BoundedContexts.Positioning.Aggregates.Carton.Services;
using LogisticsApp.Domain.BoundedContexts.Positioning.Aggregates.Warehouse.Services;
using LogisticsApp.Domain.Shared.Aggregates.User.Services;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace LogisticsApp.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        // Register infrastructure services here
        services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(DependencyInjection).Assembly));
        services.AddScoped<ProductFactory>();
        services.AddScoped<OrderCreationService>();
        services.AddScoped<OrderReturnCreationService>();
        services.AddScoped(
            typeof(IPipelineBehavior<,>),
            typeof(ValidateBehavior<,>));
        services.AddScoped<IProductUniquenessChecker, ProductUniquenessChecker>();
        services.AddScoped<IUserUniquenessChecker, UserUniquenessChecker>();
        services.AddScoped<ICartonLocationUniquenessChecker, CartonLocationUniquenessChecker>();
        services.AddScoped<IWarehouseUniquenessChecker, WarehouseUniquenessChecker>();
        services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
        services.AddScoped<CartonLocationAssigner>();
        services.AddScoped<EnforceProductInvariantsAndGetVariationRefCodeService>();
        services.AddScoped<IProductAvailabilityChecker, ProductAvailabilityChecker>();
        services.AddScoped<IOrderReturnItemsValidation, OrderReturnItemsValidation>();

        return services;
    }
}