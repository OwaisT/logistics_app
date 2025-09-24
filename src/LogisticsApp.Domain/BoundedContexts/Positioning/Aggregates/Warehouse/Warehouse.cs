using ErrorOr;
using LogisticsApp.Domain.BoundedContexts.Positioning.Aggregates.Warehouse.Entities;
using LogisticsApp.Domain.BoundedContexts.Positioning.Aggregates.Warehouse.Services;
using LogisticsApp.Domain.BoundedContexts.Positioning.Aggregates.Warehouse.ValueObjects;
using LogisticsApp.Domain.Common.Errors;
using LogisticsApp.Domain.Common.Models;

namespace LogisticsApp.Domain.BoundedContexts.Positioning.Aggregates.Warehouse;

public sealed class Warehouse : AggregateRoot<WarehouseId, Guid>
{
    private readonly IWarehouseUniquenessChecker _uniquenessChecker;
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
        string country,
        IWarehouseUniquenessChecker uniquenessChecker)
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
        _uniquenessChecker = uniquenessChecker;
    }
    public static ErrorOr<Warehouse> Create(
        string name,
        string street,
        string area,
        string city,
        string postcode,
        string country,
        IWarehouseUniquenessChecker uniquenessChecker)
    {
        var warehouseId = WarehouseId.CreateUnique();
        if (!uniquenessChecker.IsUnique(name, street, area, city, postcode, country))
        {
            return Errors.Common.DuplicateEntity("Warehouse", [$"Name: {name}", $"Street: {street}", $"Area: {area}", $"City: {city}", $"Postcode: {postcode}", $"Country: {country}"]);
        }
        return new Warehouse(warehouseId, name, street, area, city, postcode, country, uniquenessChecker);
    }

    public ErrorOr<Warehouse> AddWarehouseRoom(string roomName)
    {
        if (_rooms.Any(r => r.Name.Equals(roomName, StringComparison.OrdinalIgnoreCase)))
        {
            return Errors.Common.DuplicateEntity("Room", [$"Name: {roomName}"]);
        }

        var room = Room.Create(roomName);
        _rooms.Add(room);
        return this;
    }

    public ErrorOr<Warehouse> RemoveRoom(RoomId roomId)
    {
        var room = _rooms.SingleOrDefault(r => r.Id == roomId);
        if (room == null)
        {
            return Errors.Common.EntityNotFound("Room", roomId.Value.ToString());
        }
        _rooms.Remove(room);
        return this;
    }

}
