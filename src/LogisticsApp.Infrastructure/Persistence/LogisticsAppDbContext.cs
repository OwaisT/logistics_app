using LogisticsApp.Domain.BoundedContexts.Catalog.Aggregates.Product;
using LogisticsApp.Domain.BoundedContexts.Positioning.Aggregates.Warehouse;
using LogisticsApp.Domain.Common.Models;
using LogisticsApp.Domain.Shared.Aggregates.User;
using LogisticsApp.Infrastructure.Persistence.Interceptors;
using LogisticsApp.Infrastructure.Persistence.Users.Entities;

// using LogisticsApp.Infrastructure.Persistence.Products.Models;
using Microsoft.EntityFrameworkCore;

namespace LogisticsApp.Infrastructure.Persistence;

public class LogisticsAppDbContext : DbContext
{
    // private readonly PublishDomainEventsInterceptor _publishDomainEventsInterceptor = publishDomainEventsInterceptor;
    public LogisticsAppDbContext(
        DbContextOptions<LogisticsAppDbContext> options)
         : base(options)
    {
        // _publishDomainEventsInterceptor = publishDomainEventsInterceptor;
    }

    public DbSet<Warehouse> Warehouses { get; set; }
    public DbSet<User> Users { get; set; }
    public DbSet<RoleEntity> Roles { get; set; }
    // public DbSet<Product> Products { get; set; } = null!;
    // public DbSet<AssortmentEntry> ProductAssortments { get; set; } = null!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder
            .ApplyConfigurationsFromAssembly(typeof(LogisticsAppDbContext).Assembly);
        base.OnModelCreating(modelBuilder);
    }

    // protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    // {
    //     optionsBuilder.AddInterceptors(_publishDomainEventsInterceptor);
    //     base.OnConfiguring(optionsBuilder);
    // }

}