# Domain Models

## Return

```csharp
class Return
{
    // Factory method to create a new Return
    // Validations can be added here as needed
    Return Create(
        Guid id,
        Guid orderId,
        List<ReturnItem> items,
        decimal totalValue,
        int totalItems,
        string status);

    void UpdateStatus(string status);
    
}

class ReturnItem
{
    Guid productId;
    Guid variationId;
    string ref;
    int quantity;

    // Validation to ensure that item existed in the original order can be added here
    ReturnItem Create(
        Guid productId,
        Guid variationId,
        string ref,
        int quantity);
}
```

```json
{
    id: "AD3E1234-5678-90AB-CDEF-1234567890AB",
    orderId: "BD4E1234-5678-90AB-CDEF-1234567890AB",
    items: [
        {
            productId: "e7a1c2b3-4f56-4d8a-9c2e-123456789aba",
            variationId: "e7a1c2b3-4f56-4d8a-9c2e-123456789add",
            ref: "PROD-001-SS2025-RED-M",
            quantity: 2
        },
        {
            productId: "e7a1c2b3-4f56-4d8a-9c2e-123456789aba",
            variationId: "e7a1c2b3-4f56-4d8a-9c2e-123456789ade",
            ref: "PROD-001-SS2025-BLUE-L",
            quantity: 1
        }
    ],
    totalItems: 3,
    totalValue: 149.97,
    status: "Processing"
}
