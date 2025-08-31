// Value objects used for product identification and variations
using LogisticsApp.Domain.ValueObjects;

// Namespace for domain entities
namespace LogisticsApp.Domain.Entities;

// Represents a product with variations, colors, sizes, and assortment
public class Product
{
    // Unique identifier for the product (composed of productRef and season)
    public ProductId Id { get; private set; }
    // Reference code for the product
    public string ProductRef { get; private set; } = null!;
    // Season for which the product is available
    public string Season { get; private set; } = null!;
    // List of available colors for the product
    public List<string> Colors { get; private set; } = [];
    // List of available sizes for the product
    public List<string> Sizes { get; private set; } = [];
    // Assortment: size -> quantity
    // Dictionary mapping size to quantity (general assortment)
    public Dictionary<string, int> GeneralAssortment { get; private set; } = new();
    // List of product variations (color, size, etc.)
    public List<Variation> Variations { get; private set; } = [];

    // Constructor initializes product with reference and season
    public Product(string productRef, string season)
    {
        Id = new ProductId(productRef, season);
        ProductRef = productRef;
        Season = season;
    }

    // Adds a color to the product if not already present
    public void AddColor(string color)
    {
        if (!Colors.Contains(color))
            Colors.Add(color);
    }

    // Adds a size to the product if not already present
    public void AddSize(string size)
    {
        if (!Sizes.Contains(size))
            Sizes.Add(size);
    }

    // Sets the general assortment for the product
    public void SetAssortment(Dictionary<string, int> assortment)
    {
        GeneralAssortment = new Dictionary<string, int>(assortment);
    }

    // Adds a variation (color and size) to the product
    public void AddVariation(string color, string size)
    {
        Variations.Add(new Variation(color, size, ProductRef, Season));
    }
}
