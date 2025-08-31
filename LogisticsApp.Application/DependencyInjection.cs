using LogisticsApp.Application.Services.Authentication;
using Microsoft.Extensions.DependencyInjection;

namespace LogisticsApp.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        // Register infrastructure services here
        services.AddSingleton<IAuthenticationService, AuthenticationService>();
        return services;
    }
}