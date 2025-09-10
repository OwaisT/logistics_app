using LogisticsApp.Domain.Aggregates.Product;
using Microsoft.EntityFrameworkCore;

namespace LogisticsApp.Infrastructure.Persistence;

public class LogisticsAppDbContext : DbContext
{
    public LogisticsAppDbContext(DbContextOptions<LogisticsAppDbContext> options)
        : base(options)
    {
    }

    public DbSet<Product> Products { get; set;} = null!;

}