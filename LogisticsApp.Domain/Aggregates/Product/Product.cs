using LogisticsApp.Domain.Aggregates.Product.Entities;
using LogisticsApp.Domain.Aggregates.Product.ValueObjects;
using LogisticsApp.Domain.Common.Models;
using LogisticsApp.Domain.Products.ValueObjects;

namespace LogisticsApp.Domain.Aggregates.Product;

public sealed class Product : AggregateRoot<ProductId, Guid>
{
    private readonly List<Variation> variations = [];
    private readonly List<string> _categories = [];
    private readonly List<string> _colors = [];
    private readonly List<string> _sizes = [];
    private readonly List<Assortment> _assortments = [];
    public string RefCode { get; private set; }
    public string Season { get; private set; }
    public string Name { get; private set; }
    public string Description { get; private set; }
    public DateTime CreatedAt { get; private set; }
    public DateTime UpdatedAt { get; private set; }
    public bool IsActive { get; private set; }
    public IReadOnlyList<string> Categories => _categories.AsReadOnly();
    public IReadOnlyList<string> Colors => _colors.AsReadOnly();
    public IReadOnlyList<string> Sizes => _sizes.AsReadOnly();
    public IReadOnlyList<Assortment> Assortments => _assortments.AsReadOnly();
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
        List<string> sizes,
        List<Assortment> assortments)
        : base(id)
    {
        RefCode = refCode;
        Season = season;
        Name = name;
        Description = description;
        CreatedAt = createdAt;
        UpdatedAt = updatedAt;
        IsActive = isActive;
        _categories = categories;
        _colors = colors;
        _sizes = sizes;
        _assortments = assortments;
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
        List<string> sizes,
        List<Assortment> assortments)
    {
        var productId = ProductId.CreateUnique();
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
            sizes,
            assortments);
    }

    public void AddVariation(Variation variation)
    {
        variations.Add(variation);
    }

    public void RemoveVariation(Variation variation)
    {
        variations.Remove(variation);
    }

    private Product() : base(default!)
    {
        RefCode = string.Empty;
        Season = string.Empty;
        Name = string.Empty;
        Description = string.Empty;
        // Initialize other non-nullable properties as needed
    }
}
