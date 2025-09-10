using LogisticsApp.Domain.Aggregates.Product;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LogisticsApp.Infrastructure.Persistence.Configurations;

public class ProductConfigurations : IEntityTypeConfiguration<Product>
{
    public void Configure(EntityTypeBuilder<Product> builder)
    {
        ConfigureMenusTable(builder);

    }

    private void ConfigureMenusTable(EntityTypeBuilder<Product> builder)
    {
        builder.ToTable("Products");
        
    }
}