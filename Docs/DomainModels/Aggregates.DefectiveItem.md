# Domain Models

## Defective Item

```csharp
class DefectiveItem
{
    DefectiveItem Create(
        Guid id,
        ProductId productId,
        VariationId variationId,
        string ref,
        string reason,
        bool isRepairable,
        DateTime reportedAt,
        DateTime? repairedAt);

    void MarkAsRepaired(DateTime repairedAt);
    
}
```

```json
{
    id: "AD3E1234-5678-90AB-CDEF-1234567890AB",
    productId: "e7a1c2b3-4f56-4d8a-9c2e-123456789aba",
    variationId: "e7a1c2b3-4f56-4d8a-9c2e-123456789add",
    ref: "PROD-001-SS2025-RED-M",
    reason: "Skip stitching on sleeve",
    isRepairable: true,
    reportedAt: "2024-01-15T10:30:00Z",
    repairedAt: "2024-01-20T14:45:00Z"
}
```
