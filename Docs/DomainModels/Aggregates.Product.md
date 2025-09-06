# Domain Models

## Product

```csharp
class Product
{
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
    
    void UpdateStock(string variationId, int newStock);
}
```

```json
{
    id: "PROD-001 - SS2025",
    ref: "PROD-001",
    season: "2025 summer",
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
    string Id { get; }
    string Name { get; }
    decimal Price { get; }
    int Stock { get; }
    string Color { get; }
    string Size { get; }
    int Sold { get; }
    int Available { get; }

    Variation Create(
        string id, 
        string name, 
        decimal price, 
        int stock, 
        string color, 
        string size);
    
    void UpdatePrice(decimal newPrice);
    void UpdateStock(int newStock);
    void RecordSale(int quantity);

}
```

```json
{
    id: "PROD-001-SS2025-RED-M",
    name: "Sample Product Red - M",
    price: 29.99,
    stock: 100,
    color: "red",
    size: "M",
    sold: 20,
    available: 80,
    createdAt: "2023-01-01T00:00:00Z",
    updatedAt: "2023-01-01T00:00:00Z"
}
```