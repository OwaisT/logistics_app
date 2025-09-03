using System.Reflection;
using ErrorOr;
using FluentValidation;
using LogisticsApp.Application.Authentication.Commands.Register;
using LogisticsApp.Application.Authentication.Common;
using LogisticsApp.Application.Common.Behaviors;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace LogisticsApp.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        // Register infrastructure services here
        services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(DependencyInjection).Assembly));
        services.AddScoped(
            typeof(IPipelineBehavior<,>),
            typeof(ValidateBehavior<,>));
        services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
        return services;
    }
}