using LogisticsApp.Api.Common.Errors;
using LogisticsApp.Api.Common.Mapping;
using Microsoft.AspNetCore.Mvc.Infrastructure;

namespace LogisticsApp.Api;

public static class DependencyInjection
{
    public static IServiceCollection AddPresentation(this IServiceCollection services)
    {
        services.AddControllers();
        services.AddSingleton<ProblemDetailsFactory, LogisticsAppProblemDetailsFactory>();
        services.AddMappings();
        return services;
    }
}