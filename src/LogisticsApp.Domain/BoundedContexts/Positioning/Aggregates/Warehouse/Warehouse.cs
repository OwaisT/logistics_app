using LogisticsApp.Domain.BoundedContexts.Positioning.Aggregates.Warehouse.Entities;
using LogisticsApp.Domain.BoundedContexts.Positioning.Aggregates.Warehouse.ValueObjects;
using LogisticsApp.Domain.Common.Models;

namespace LogisticsApp.Domain.BoundedContexts.Positioning.Aggregates.Warehouse;

public sealed class Warehouse : AggregateRoot<WarehouseId, Guid>
{
    private readonly List<Room> _rooms = [];
    public string Name { get; private set; }
    public string Street { get; private set; }
    public string Area { get; private set; }
    public string City { get; private set; }
    public string Postcode { get; private set; }
    public string Country { get; private set; }
    public DateTime CreatedAt { get; private set; }
    public DateTime UpdatedAt { get; private set; }
    public bool IsActive { get; private set; }
    public IReadOnlyList<Room> Rooms => _rooms.AsReadOnly();

    private Warehouse(
        WarehouseId id,
        string name,
        string street,
        string area,
        string city,
        string postcode,
        string country)
        : base(id)
    {
        Name = name;
        Street = street;
        Area = area;
        City = city;
        Postcode = postcode;
        Country = country;
        CreatedAt = DateTime.UtcNow;
        UpdatedAt = DateTime.UtcNow;
        IsActive = true;
        _rooms = [];
    }

    public static Warehouse Create(
        string name,
        string street,
        string area,
        string city,
        string postcode,
        string country)
    {
        var warehouseId = WarehouseId.CreateUnique();
        return new Warehouse(warehouseId, name, street, area, city, postcode, country);
    }

    public void AddWarehouseRoom(string roomName)
    {
        var room = Room.Create(roomName);
        _rooms.Add(room);
    }

    public void RemoveRoom(RoomId roomId)
    {
        if (roomId == null)
        {
            throw new ArgumentNullException(nameof(roomId));
        }

        var room = _rooms.SingleOrDefault(r => r.Id == roomId);
        if (room == null)
        {
            // TODO: Create a custom exception for not found
            throw new ArgumentException("Room not found.", nameof(roomId));
        }

        _rooms.Remove(room);
    }

}
