# Domain Models

## Product

```csharp
class Product
{
    string Ref { get; }
    string Season { get; }
    string Name { get; }
    string Description { get; }
    DateTime CreatedAt { get; }
    DateTime UpdatedAt { get; }
    bool IsActive { get; }
    List<string> Categories { get; }
    List<string> Colors { get; }
    List<string> Sizes { get; }
    List<Variation> Variations { get; }

    Product Create(
        string ref, 
        string season, 
        string name, 
        string description, 
        bool isActive,
        List<string> categories, 
        List<string> colors, 
        List<string> sizes, 
        List<Variation> variations);
    
    void UpdateDetails(
        string name, 
        string description, 
        bool isActive, 
        List<string> categories,
        List<string> colors,
        List<string> sizes);
    
    void AddVariation(Variation variation);
    
    void RemoveVariation(string variationId);
}
```

```json
{
    id: "PROD-001-SS2025",
    ref: "PROD-001",
    season: "SS2025",
    name: "Sample Product",
    description: "This is a sample product description.",
    createdAt: "2023-01-01T00:00:00Z",
    updatedAt: "2023-01-01T00:00:00Z",
    isActive: true,
    categories: ["Men", "Women", "Kids"],
    colors: ["red", "blue", "green"],
    sizes: ["S", "M", "L", "XL", "XXL"],
    variations: [
        {
            id: "PROD-001-SS2025-RED-M",
            name: "Sample Product Red - M",
            price: 29.99,
            stock: 100
        },
        {
            id: "PROD-001-SS2025-BLUE-L",
            name: "Sample Product Blue - L",
            price: 31.99,
            stock: 50
        }
    ],
}
```

## Variation

```csharp
class Variation
{
    string Name { get; }
    string Description { get; }
    decimal Price { get; }
    string Color { get; }
    string Size { get; }
    int Received { get; }
    int Sold { get; }
    int Available { get; }
    DateTime CreatedAt { get; }
    DateTime UpdatedAt { get; }

    Variation Create(
        string name, 
        decimal price, 
        string color, 
        string size);
    
    void UpdatePrice(decimal newPrice);
    void IncreaseReceived(int newReceived);
    void RecordSale(int quantity);

}
```

```json
{
    id: "PROD-001-SS2025-RED-M",
    name: "Sample Product Red - M",
    description: "This is a sample product variation description.",
    price: 29.99,
    color: "red",
    size: "M",
    received: 100,
    sold: 20,
    available: 80,
    createdAt: "2023-01-01T00:00:00Z",
    updatedAt: "2023-01-01T00:00:00Z"
}
```