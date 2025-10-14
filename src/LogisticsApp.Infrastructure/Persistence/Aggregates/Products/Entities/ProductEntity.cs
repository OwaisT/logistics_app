using LogisticsApp.Domain.BoundedContexts.Catalog.Aggregates.ProductAggregate.ValueObjects;

namespace LogisticsApp.Infrastructure.Persistence.Aggregates.Products.Entities;

public class ProductEntity
{
    public Guid Id { get; set; }

    public string RefCode { get; set; } = null!;
    public string Season { get; set; } = null!;
    public string Name { get; set; } = null!;
    public string Description { get; set; } = null!;
    public decimal GeneralPrice { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
    public bool IsActive { get; set; } = true;
    // If you keep the existing JSON mapping for assortments, keep the same CLR type
    // (Assortment is a domain value object type used previously in ProductConfigurations).
    // ProductConfigurations will handle JSON conversion.
    public List<Assortment> Assortments { get; set; } = [];
    // If Variations are modeled as owned/complex types, you can either map them as
    // an owned collection (OwnsMany) or provide a VariationEntity type (example below).
    public List<VariationEntity> Variations { get; set; } = [];
    // Navigation (many-to-many) collections for EF Core to track.
    // These are the persistence entities for Category/Color/Size (already exist in infra).
    public ICollection<CategoryEntity> Categories { get; set; } = [];
    public ICollection<ColorEntity> Colors { get; set; } = [];
    public ICollection<SizeEntity> Sizes { get; set; } = [];

    // Convenience: optional factory to convert from domain ProductId if you want.
    public static ProductEntity FromDomainId(ProductId domainId)
        => new() { Id = domainId.Value };
}
