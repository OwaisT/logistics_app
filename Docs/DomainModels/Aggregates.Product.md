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
    void ProcessReturn(int quantity);
    void ReportDefect(int quantity);
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
    categories: ["Men"],
    colors: ["red", "blue", "green"],
    sizes: ["S", "M", "L", "XL", "XXL"],
    isAssortmentIdentical: false,
    assortments : [
        {
            color: "red",
            assortment : {
                s: 1,
                m: 2,
                l: 3,
                xl: 2,
                xxl: 1
            }
        },
        {
            color: "blue",
            assortment : {
                s: 1,
                m: 2,
                l: 2,
                xl: 2,
                xxl: 1
            }
        },
        {
            color: "green",
            assortment : {
                s: 1,
                m: 2,
                l: 3,
                xl: 2,
                xxl: 1
            }
        }
    ],
    variations: [
        {
            id: "PROD-001-SS2025-RED-M",
            name: "Sample Product Red - M",
            description: "This is a sample product variation description.",
            price: 29.99,
            color: "red",
            size: "M",
            received: 100,
            sold: 20,
            available: 83 (after returns and defects, i.e., 100 - 20 + 5 - 2),
            returned: 5,
            defective: 2,
            createdAt: "2023-01-01T00:00:00Z",
            updatedAt: "2023-01-01T00:00:00Z"
        },
        {
            id: "PROD-001-SS2025-BLUE-L",
            name: "Sample Product Blue - L",
            description: "This is a sample product variation description.",
            price: 31.99,
            color: "blue",
            size: "L",
            received: 50,
            sold: 10,
            available: 41 (after returns and defects, i.e., 50 - 10 + 2 - 1),
            returned: 2,
            defective: 1,
            createdAt: "2023-01-01T00:00:00Z",
            updatedAt: "2023-01-01T00:00:00Z"
        }
    ],
}
```
