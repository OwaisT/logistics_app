using LogisticsApp.Domain.Aggregates.Product.Entities;
using LogisticsApp.Domain.Aggregates.Product.ValueObjects;
using LogisticsApp.Domain.Common.Models;

namespace LogisticsApp.Domain.Aggregates.Product;

public sealed class Product : AggregateRoot<ProductId>
{
    private readonly List<Variation> variations = new();
    private readonly List<string> categories = new();
    private readonly List<string> colors = new();
    private readonly List<string> sizes = new();
    public ProductId ProductId => Id;
    public string RefCode { get; private set; }
    public string Season { get; private set; }
    public string Name { get; private set; }
    public string Description { get; private set; }
    public DateTime CreatedAt { get; private set; }
    public DateTime UpdatedAt { get; private set; }
    public bool IsActive { get; private set; }
    public IReadOnlyList<string> Categories => categories.AsReadOnly();
    public IReadOnlyList<string> Colors => colors.AsReadOnly();
    public IReadOnlyList<string> Sizes => sizes.AsReadOnly();
    public IReadOnlyList<Variation> Variations => variations.AsReadOnly();

    private Product(
        ProductId id,
        string refCode,
        string season,
        string name,
        string description,
        DateTime createdAt,
        DateTime updatedAt,
        bool isActive,
        List<string> categories,
        List<string> colors,
        List<string> sizes)
        : base(id)
    {
        RefCode = refCode;
        Season = season;
        Name = name;
        Description = description;
        CreatedAt = createdAt;
        UpdatedAt = updatedAt;
        IsActive = isActive;
        this.categories = categories;
        this.colors = colors;
        this.sizes = sizes;
        variations = [];
    }

    public static Product Create(
        string refCode,
        string season,
        string name,
        string description,
        bool isActive,
        List<string> categories,
        List<string> colors,
        List<string> sizes)
    {
        var productId = ProductId.Create(refCode, season);
        return new(
            productId,
            refCode,
            season,
            name,
            description,
            DateTime.UtcNow,
            DateTime.UtcNow,
            isActive,
            categories,
            colors,
            sizes);
    }

    public void AddVariation(Variation variation)
    {
        variations.Add(variation);
    }

    public void RemoveVariation(Variation variation)
    {
        variations.Remove(variation);
    }
}
