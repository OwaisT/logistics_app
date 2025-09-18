# Domain Models

## Carton

```csharp
class Carton
{
    Carton Create(
        Guid id,
        Location location);

    List<CartonItem> items;
    
    // Validated by domain service which verifies no 2 cartons have same location 
    void UpdateLocation(Location location);
    
    void AddItem(CartonItem item);
    
    void RemoveItem(Guid productId, Guid variationId, int quantityRemoved)
    {
        // if quantityRemoved > existing quantity, throw error
        // if quantityRemoved == existing quantity, remove item from list
        // if quantityRemoved < existing quantity, reduce quantity (It is a value object so we need to replace it, not modify it)

    }
    
    void Activate();
    
    void Deactivate();
}

class Location
{
    Guid warehouseId;
    Guid roomId;
    int onLeft;
    int below;
    int behind;

    Location Create(
        Guid warehouseId,
        Guid roomId,
        int onLeft,
        int below,
        int behind);
}

class CartonItem
{
    Guid productId;
    Guid variationId;
    string ref;
    int quantity;

    CartonItem Create(
        Guid productId,
        Guid variationId,
        string ref,
        int quantity);
}
```

```json
{
    id: "AD3E1234-5678-90AB-CDEF-1234567890AB",
    location : { 
        warehouseId: "e7a1c2b3-4f56-4d8a-9c2e-123456789abc", 
        roomId: "AD3E1234-5678-90AB-CDEF-1234567890AB",
        onLeft: 0, 
        below: 0, 
        behind: 0 
    },
    items: [
        {
            productId: "e7a1c2b3-4f56-4d8a-9c2e-123456789aba",
            variationId: "e7a1c2b3-4f56-4d8a-9c2e-123456789add",
            ref: "PROD-001-SS2025-RED-M",
            quantity: 4
        },
        {
            productId: "e7a1c2b3-4f56-4d8a-9c2e-123456789aba",
            variationId: "e7a1c2b3-4f56-4d8a-9c2e-123456789ade",
            ref: "PROD-001-SS2025-BLUE-L",
            quantity: 2
        }
    ],
}
