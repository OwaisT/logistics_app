using System.Reflection;
using ErrorOr;
using FluentValidation;
using LogisticsApp.Application.Authentication.Commands.Register;
using LogisticsApp.Application.Authentication.Common;
using LogisticsApp.Application.Common.Behaviors;
using LogisticsApp.Application.Products.Services;
using LogisticsApp.Domain.Aggregates.Product;
using LogisticsApp.Domain.Aggregates.Product.Services;
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
        services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
        return services;
    }
}