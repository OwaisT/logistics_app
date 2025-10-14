namespace LogisticsApp.Infrastructure.Persistence.Aggregates.Products.Entities;

public class VariationEntity
{
    public Guid Id { get; set; }
    public string ProductRefCode { get; set; } = null!;
    public string ProductSeason { get; set; } = null!;
    public string VariationRefCode { get; set; } = null!;
    public string Name { get; set; } = null!;
    public string Description { get; set; } = null!;
    public decimal Price { get; set; }
    public string Color { get; set; } = null!;
    public string Size { get; set; } = null!;
    public int Received { get; set; }
    public int Sold { get; set; }
    public int Available { get; set; }
    public int Returned { get; set; }
    public int Defective { get; set; }
    public int Repaired { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }

}