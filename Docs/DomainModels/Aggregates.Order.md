# Domain Models

## Order

```csharp
class Order
{
    // Factory method to create a new Order
    // Validations can be added here as needed
    Order Create(
        Guid id,
        List<OrderItem> items,
        decimal totalValue,
        int totalItems,
        string status);

    void UpdateStatus(string status);
    
}

class OrderItem
{
    Guid productId;
    Guid variationId;
    string ref;
    int quantity;

    OrderItem Create(
        Guid productId,
        Guid variationId,
        string ref,
        int quantity);
}
```

```json
{
    id: "AD3E1234-5678-90AB-CDEF-1234567890AB",
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
    totalItems: 6,
    totalValue: 299.94,
    status: "Processing"
}
