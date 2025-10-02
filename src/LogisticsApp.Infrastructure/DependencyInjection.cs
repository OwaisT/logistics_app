using System.Text;
using LogisticsApp.Application.Common.Interfaces.Authentication;
using LogisticsApp.Application.Common.Interfaces.Persistence;
using LogisticsApp.Application.Common.Interfaces.Services;
using LogisticsApp.Infrastructure.Authentication;
using LogisticsApp.Infrastructure.Persistence;
using LogisticsApp.Infrastructure.Persistence.Interceptors;
using LogisticsApp.Infrastructure.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace LogisticsApp.Infrastructure;

using LogisticsApp.Infrastructure.Persistence.Aggregates.Cartons.Repositories;
using LogisticsApp.Infrastructure.Persistence.Aggregates.OrderReturns.Repositories;
using LogisticsApp.Infrastructure.Persistence.Aggregates.Orders.Repositories;
using LogisticsApp.Infrastructure.Persistence.Aggregates.Products.Helpers;
using LogisticsApp.Infrastructure.Persistence.Aggregates.Products.Repositories;
using LogisticsApp.Infrastructure.Persistence.Aggregates.Users.Helpers;
using LogisticsApp.Infrastructure.Persistence.Aggregates.Users.Repositories;
using LogisticsApp.Infrastructure.Persistence.Aggregates.Warehouses.Repositories;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(
        this IServiceCollection services,
        ConfigurationManager configuration
    )
    {
        services
            .AddAuth(configuration)
            .AddPersistance(configuration);
        services.AddSingleton<IDateTimeProvider, DateTimeProvider>();


        return services;
    }

    public static IServiceCollection AddPersistance(
        this IServiceCollection services,
        ConfigurationManager configuration)
    {
        var connectionString = configuration.GetConnectionString("DefaultConnection");
        services.AddDbContext<LogisticsAppDbContext>(options =>
            options.UseNpgsql(connectionString));
        services.AddScoped<PublishDomainEventsInterceptor>();
        services.AddRepositories();
        services.AddHelpers();

        return services;
    }

    public static IServiceCollection AddAuth(
        this IServiceCollection services,
        ConfigurationManager configuration)
    {
        var jwtSettings = new JwtSettings();
        configuration.Bind(JwtSettings.SectionName, jwtSettings);

        services.AddSingleton(Options.Create(jwtSettings));
        services.AddSingleton<IJwtTokenGenerator, JWTTokenGenerator>();

        services.AddAuthentication(defaultScheme: JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(options => options.TokenValidationParameters = new()
            {
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true,
                ValidIssuer = jwtSettings.Issuer,
                ValidAudience = jwtSettings.Audience,
                IssuerSigningKey = new SymmetricSecurityKey(
                    Encoding.UTF8.GetBytes(jwtSettings.SecretKey)
                ),
                ClockSkew = TimeSpan.Zero
            });

        return services;
    }

    public static IServiceCollection AddRepositories(this IServiceCollection services)
    {
        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<IProductRepository, ProductRepository>();
        services.AddScoped<IWarehouseRepository, WarehouseRepository>();
        services.AddScoped<ICartonRepository, CartonRepository>();
        services.AddScoped<IOrderRepository, OrderRepository>();
        services.AddScoped<IOrderReturnRepository, OrderReturnRepository>();
        return services;
    }
    
    public static IServiceCollection AddHelpers(this IServiceCollection services)
    {
        services.AddScoped<UserMappingInHelper>();
        services.AddScoped<UserDBInsertionHelper>();
        services.AddScoped<UserMappingOutHelper>();
        services.AddScoped<UserDBExtractionHelper>();
        services.AddScoped<ProductMappingInHelper>();
        services.AddScoped<ProductDBInsertionHelper>();
        services.AddScoped<ProductMappingOutHelper>();
        services.AddScoped<ProductDBExtractionHelper>();
        return services;
    }
}