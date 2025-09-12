using LogisticsApp.Domain.Aggregates.Product;
using LogisticsApp.Infrastructure.Persistence.Products.Models;
using Microsoft.EntityFrameworkCore;

namespace LogisticsApp.Infrastructure.Persistence;

public class LogisticsAppDbContext : DbContext
{
    public LogisticsAppDbContext(DbContextOptions<LogisticsAppDbContext> options)
        : base(options)
    {
    }

    public DbSet<Product> Products { get; set; } = null!;
    public DbSet<AssortmentEntry> ProductAssortments { get; set; } = null!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder
            .ApplyConfigurationsFromAssembly(typeof(LogisticsAppDbContext).Assembly);
        base.OnModelCreating(modelBuilder);
    }

}