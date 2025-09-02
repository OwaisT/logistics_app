using LogisticsApp.Application.Authentication;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace LogisticsApp.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        // Register infrastructure services here
        services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(DependencyInjection).Assembly));
        return services;
    }
}