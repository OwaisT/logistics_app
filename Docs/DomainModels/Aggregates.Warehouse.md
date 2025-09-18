# Domain Models

## Warehouse

```csharp
class Warehouse
{
    Warehouse Create(
        Guid id,
        string name,
        string street,
        string area,
        string city,
        string postcode,
        string country);
    
    void UpdateName(string name);

    void UpdateAddress(
        string street,
        string area,
        string areaCode,
        string city,
        string cityCode,
        string postcode,
        string country,
        string countryCode);
    
    void CreateUniqueRoom(string name);

    void RemoveRoom(Guid roomId);

    void Activate();
    
    void Deactivate();
}

class Room
{
    Guid id;
    string name;

    Room Create(Guid id, string name);
    
    void UpdateName(string name);

}
```

```json
{
    "id": "e7a1c2b3-4f56-4d8a-9c2e-123456789abc",
    "name": "Paris Haussmann Warehouse",
    "street": "64 Boulevard Haussmann",
    "area": "Haussmann",
    "city": "Paris",
    "postcode": "75009",
    "country": "France",
    "createdAt": "2023-01-01T00:00:00Z",
    "updatedAt": "2023-01-01T00:00:00Z",
    "isActive": true,
    "rooms": [
        {
            "id": "AD3E1234-5678-90AB-CDEF-1234567890AB",
            "name": "Room 1"
        },
        {
            "id": "BD3E1234-5678-90AB-CDEF-1234567890AB",
            "name": "Room 2"
        },
        {
            "id": "CD3E1234-5678-90AB-CDEF-1234567890AB",
            "name": "Room 3"
        }
    ]
}
```
