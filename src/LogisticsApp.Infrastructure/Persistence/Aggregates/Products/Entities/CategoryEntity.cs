namespace LogisticsApp.Infrastructure.Persistence.Aggregates.Products.Entities;

public class CategoryEntity
{
    public Guid Id { get; set; }
    public string Name { get; set; } = null!;
}
