using System.Reflection;
using ErrorOr;
using FluentValidation;
using LogisticsApp.Application.Authentication.Commands.Register;
using LogisticsApp.Application.Authentication.Common;
using LogisticsApp.Application.Authentication.Services;
using LogisticsApp.Application.Cartons.Services;
using LogisticsApp.Application.Common.Behaviors;
using LogisticsApp.Application.Products.Services;
using LogisticsApp.Application.Warehouses.Services;
using LogisticsApp.Domain.BoundedContexts.Catalog.Aggregates.Product;
using LogisticsApp.Domain.BoundedContexts.Catalog.Aggregates.Product.Services;
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
        services.AddScoped(
            typeof(IPipelineBehavior<,>),
            typeof(ValidateBehavior<,>));
        services.AddScoped<IProductUniquenessChecker, ProductUniquenessChecker>();
        services.AddScoped<IUserUniquenessChecker, UserUniquenessChecker>();
        services.AddScoped<ICartonLocationUniquenessChecker, CartonLocationUniquenessChecker>();
        services.AddScoped<IWarehouseUniquenessChecker, WarehouseUniquenessChecker>();
        services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
        services.AddScoped<CartonLocationAssigner>();
        return services;
    }
}