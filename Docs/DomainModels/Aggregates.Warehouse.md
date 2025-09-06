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
```

```json
{
    id: "FR-PAR-HAU-001",
    countryCode: "FR",
    cityCode: "PAR",
    areaCode: "HAU",
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
        },
        {
            id: "FR-PAR-HAU-001-RM-002",
            name: "Room 2",
            totalCartons: 200
        }
    ]
}
```

## Rooms

```csharp
class Room
{
    Room Create(
        string id,
        string name,
        int totalCartons);
    
    void UpdateDetails(
        string name,
        int totalCartons);
    
    void AddCarton(Carton carton);
    
    void RemoveCarton(string cartonId);
}
```

```json
{
    id: "FR-PAR-HAU-001-RM-001",
    name: "Room 1",
    totalCartons: 300,
    cartons : [
        {
            id: "00000000001",
            location : { onLeft: 0, below: 0, behind: 0 },
            totalItems: 18
        }
    ]
}
```

## Cartons

```csharp
class Carton
{
    Carton Create(
        string id,
        Location location);
    
    void UpdateLocation(Location location);
    
    void AddItem(CartonItem item);
    
    void RemoveItem(string itemId);
}
```

```json
{
    id: "00000000001",
    location : {
        onLeft : 0,
        below : 0,
        behind : 0
    },
    totalItems: 18,
    cartonItems: [
        {
            VariationId: "PROD-001-SS2025-RED-S",
            quantity: 3,
            addedAt: "2023-01-01T00:00:00Z",
            updatedAt: "2023-01-01T00:00:00Z"
        },
        {
            VariationId: "PROD-001-SS2025-RED-M",
            quantity: 4,
            addedAt: "2023-01-01T00:00:00Z",
            updatedAt: "2023-01-01T00:00:00Z"
        },
        {
            VariationId: "PROD-001-SS2025-RED-L",
            quantity: 4,
            addedAt: "2023-01-01T00:00:00Z",
            updatedAt: "2023-01-01T00:00:00Z"
        },
        {
            VariationId: "PROD-001-SS2025-RED-XL",
            quantity: 4,
            addedAt: "2023-01-01T00:00:00Z",
            updatedAt: "2023-01-01T00:00:00Z"
        },
        {
            VariationId: "PROD-001-SS2025-RED-XXL",
            quantity: 3,
            addedAt: "2023-01-01T00:00:00Z",
            updatedAt: "2023-01-01T00:00:00Z"
        }
    ]
}
```