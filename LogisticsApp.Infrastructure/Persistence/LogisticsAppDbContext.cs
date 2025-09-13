using LogisticsApp.Domain.Aggregates.Product;
using LogisticsApp.Domain.Common.Models;
using LogisticsApp.Infrastructure.Persistence.Interceptors;
using LogisticsApp.Infrastructure.Persistence.Products.Models;
using Microsoft.EntityFrameworkCore;

namespace LogisticsApp.Infrastructure.Persistence;

public class LogisticsAppDbContext : DbContext
{
    private readonly PublishDomainEventsInterceptor _publishDomainEventsInterceptor;

    public LogisticsAppDbContext(DbContextOptions<LogisticsAppDbContext> options, PublishDomainEventsInterceptor publishDomainEventsInterceptor)
        : base(options)
    {
        _publishDomainEventsInterceptor = publishDomainEventsInterceptor;
    }

    public DbSet<Product> Products { get; set; } = null!;
    public DbSet<AssortmentEntry> ProductAssortments { get; set; } = null!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder
            .Ignore<List<IDomainEvent>>()
            .ApplyConfigurationsFromAssembly(typeof(LogisticsAppDbContext).Assembly);
        base.OnModelCreating(modelBuilder);
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.AddInterceptors(_publishDomainEventsInterceptor);
        base.OnConfiguring(optionsBuilder);
    }

}