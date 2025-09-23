using LogisticsApp.Domain.BoundedContexts.Catalog.Aggregates.Product.Entities;
using LogisticsApp.Domain.BoundedContexts.Catalog.Aggregates.Product.ValueObjects;
using LogisticsApp.Domain.Common.Models;

namespace LogisticsApp.Domain.BoundedContexts.Catalog.Aggregates.Product;

public sealed class Product : AggregateRoot<ProductId, Guid>
{
    private readonly List<Variation> _variations = [];
    private readonly List<string> _categories = [];
    private readonly List<string> _colors = [];
    private readonly List<string> _sizes = [];
    private readonly List<Assortment> _assortments = [];
    public string RefCode { get; private set; }
    public string Season { get; private set; }
    public string Name { get; private set; }
    public string Description { get; private set; }
    public decimal GeneralPrice { get; private set; }
    public DateTime CreatedAt { get; private set; }
    public DateTime UpdatedAt { get; private set; }
    public bool IsActive { get; private set; }
    public IReadOnlyList<string> Categories => _categories.AsReadOnly();
    public IReadOnlyList<string> Colors => _colors.AsReadOnly();
    public IReadOnlyList<string> Sizes => _sizes.AsReadOnly();
    public IReadOnlyList<Assortment> Assortments => _assortments.AsReadOnly();
    public IReadOnlyList<Variation> Variations => _variations.AsReadOnly();

    internal Product(
        ProductId id,
        string refCode,
        string season,
        string name,
        string description,
        decimal generalPrice,
        DateTime createdAt,
        DateTime updatedAt,
        bool isActive,
        List<string> categories,
        List<string> colors,
        List<string> sizes,
        List<Assortment> assortments,
        List<Variation> variations)
        : base(id)
    {
        RefCode = refCode;
        Season = season;
        Name = name;
        Description = description;
        GeneralPrice = generalPrice;
        CreatedAt = createdAt;
        UpdatedAt = updatedAt;
        IsActive = isActive;
        _categories = categories;
        _colors = colors;
        _sizes = sizes;
        _assortments = assortments;
        _variations = variations;
    }

    public void AddVariation(Variation variation)
    {
        _variations.Add(variation);
    }

    public void RemoveVariation(Variation variation)
    {
        _variations.Remove(variation);
    }

    public void IncreaseReceivedForVariation(VariationId variationId, int quantity)
    {
        var variation = _variations.FirstOrDefault(v => v.Id == variationId);
        // TODO: handle errors appropriately
        if (variation == null)
        {
            throw new ArgumentException("Variation not found.", nameof(variationId));
        }
        variation.IncreaseReceived(quantity);
        UpdatedAt = DateTime.UtcNow;
    }

#pragma warning disable CS8618
    private Product() : base(default!) { }
#pragma warning restore CS8618
}
