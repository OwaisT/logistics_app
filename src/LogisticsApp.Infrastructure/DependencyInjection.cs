using System.Text;
using LogisticsApp.Application.Common.Interfaces.Authentication;
using LogisticsApp.Application.Common.Interfaces.Persistence;
using LogisticsApp.Application.Common.Interfaces.Services;
using LogisticsApp.Domain.Aggregates.Product;
using LogisticsApp.Infrastructure.Authentication;
using LogisticsApp.Infrastructure.Persistence;
using LogisticsApp.Infrastructure.Persistence.Interceptors;
using LogisticsApp.Infrastructure.Persistence.Products.Repositories;
using LogisticsApp.Infrastructure.Persistence.Users.Repositories;
using LogisticsApp.Infrastructure.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace LogisticsApp.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(
        this IServiceCollection services,
        ConfigurationManager configuration
    )
    {
        services
            .AddAuth(configuration)
            .AddPersistance();
        services.AddSingleton<IDateTimeProvider, DateTimeProvider>();

        
        return services;
    }

    public static IServiceCollection AddPersistance(
        this IServiceCollection services)
    {
        services.AddDbContext<LogisticsAppDbContext>(options => 
            options.UseSqlServer("Server=localhost;Database=LogisticsApp;User Id=sa;Password=Orb_One123;TrustServerCertificate=True"));
        services.AddScoped<PublishDomainEventsInterceptor>();
        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<IProductRepository, ProductRepository>();
        services.AddScoped<IProductAggregateRepository, ProductAggregateRepository>();
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
}