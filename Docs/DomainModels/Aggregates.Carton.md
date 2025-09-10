# Domain Models

## Carton

```csharp
class Carton
{
    Carton Create(
        Guid id,
        Location location);
    
    void UpdateLocation(Location location);
    
    void AddItem(CartonItem item);
    
    void RemoveItem(Guid itemId);
    
    void Activate();
    
    void Deactivate();
}
```

```json
{
    id: "AD3E1234-5678-90AB-CDEF-1234567890AB",
    location : { 
        warehouseId: "FR-PAR-HAU-001", 
        roomId: "FR-PAR-HAU-001-RM-001",
        onLeft: 0, 
        below: 0, 
        behind: 0 
    },
    items: [
        {
            variationId: "PROD-001-SS2025-BLUE-L",
            quantity: 4
        },
        {
            variationId: "PROD-001-SS2025-BLUE-M",
            quantity: 2
        }
    ],
}
