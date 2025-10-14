namespace LogisticsApp.Infrastructure.Persistence.Aggregates.Products.Entities;

public class ColorEntity
{
    public Guid Id { get; set; }
    public string Name { get; set; } = null!;
    public ICollection<ProductEntity> Products { get; set; } = [];

}
