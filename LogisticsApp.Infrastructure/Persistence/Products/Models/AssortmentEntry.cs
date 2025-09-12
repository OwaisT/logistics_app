using LogisticsApp.Domain.Aggregates.Product.ValueObjects;

namespace LogisticsApp.Infrastructure.Persistence.Products.Models;
public class AssortmentEntry
{
    public ProductId ProductId { get; set; } = default!;
    public string Color { get; set; } = default!;
    public string Size { get; set; } = default!;
    public int Quantity { get; set; }
}
