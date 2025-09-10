# Domain Models

## Warehouse

```csharp
class Warehouse
{
    Warehouse Create(
        string countryCode,
        string cityCode,
        string areaCode,
        string name,
        string city,
        string postcode,
        string country,
        string area,
        string location);
    
    void UpdateDetails(
        string name,
        string city,
        string postcode,
        string country,
        string area,
        string location);
    
    void AddRoom(Room room);
    
    void RemoveRoom(string roomId);
    
    void Activate();
    
    void Deactivate();
}

class Room
{
    Room Create(
        string id,
        string name,
        int totalCartons);
    
    void UpdateDetails(
        string name,
        int totalCartons);
    
    void AddCarton(string cartonId);
    
    void RemoveCarton(string cartonId);
}
```

```json
{
    id: "FR-PAR-HAU-001",
    countryCode: "FR",
    cityCode: "PAR",
    areaCode: "HAU",
    uniqueNumber: "001",
    name: "Paris Haussmann Warehouse",
    city: "Paris",
    postcode: "75009",
    country: "France",
    area: "Haussmann"
    location: "64 Boulevard Haussmann",
    createdAt: "2023-01-01T00:00:00Z",
    updatedAt: "2023-01-01T00:00:00Z",
    isActive: true,
    rooms: [
        {
            id: "FR-PAR-HAU-001-RM-001",
            name: "Room 1",
            totalCartons: 300
            cartons : [
                "AD3E1234-5678-90AB-CDEF-1234567890AB",
                "BD3E1234-5678-90AB-CDEF-1234567890AB",
                "CD3E1234-5678-90AB-CDEF-1234567890AB" 
            ]
        },
        {
            id: "FR-PAR-HAU-001-RM-002",
            name: "Room 2",
            totalCartons: 200,
            cartons : [
                "AD3E1234-5678-90AB-CDEF-1234567890AB",
                "BD3E1234-5678-90AB-CDEF-1234567890AB",
                "CD3E1234-5678-90AB-CDEF-1234567890AB" 
            ]
        }
    ]
}
```
