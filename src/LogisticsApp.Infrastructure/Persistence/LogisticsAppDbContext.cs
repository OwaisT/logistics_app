using LogisticsApp.Domain.BoundedContexts.Positioning.Aggregates.Carton;
using LogisticsApp.Domain.BoundedContexts.Positioning.Aggregates.Warehouse;
using LogisticsApp.Domain.Shared.Aggregates.User;
using LogisticsApp.Domain.Common.Models;
using LogisticsApp.Infrastructure.Persistence.Interceptors;

// using LogisticsApp.Infrastructure.Persistence.Products.Models;
using Microsoft.EntityFrameworkCore;
using LogisticsApp.Infrastructure.Persistence.Aggregates.Products.Entities;
using LogisticsApp.Infrastructure.Persistence.Aggregates.Users.Entities;
using LogisticsApp.Domain.BoundedContexts.Catalog.Aggregates.OrderReturnAggregate;
using LogisticsApp.Domain.BoundedContexts.Catalog.Aggregates.ProductAggregate;
using LogisticsApp.Domain.BoundedContexts.Catalog.Aggregates.OrderAggregate;

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
    public DbSet<Product> Products { get; set; }
    public DbSet<SizeEntity> ProductSizes { get; set; }
    public DbSet<ColorEntity> ProductColors { get; set; }
    public DbSet<CategoryEntity> ProductCategories { get; set; }
    public DbSet<Carton> Cartons { get; set; }
    public DbSet<Order> Orders { get; set; }
    public DbSet<OrderReturn> OrderReturns { get; set; }

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