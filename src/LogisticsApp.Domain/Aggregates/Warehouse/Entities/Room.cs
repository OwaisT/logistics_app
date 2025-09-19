using LogisticsApp.Domain.Aggregates.Warehouse.ValueObjects;
using LogisticsApp.Domain.Common.Exceptions;
using LogisticsApp.Domain.Common.Models;

namespace LogisticsApp.Domain.Aggregates.Warehouse.Entities;

public sealed class Room : Entity<RoomId>
{
    public string Name { get; private set; }

    private Room(
        RoomId id,
        string name)
        : base(id)
    {
        Name = name;
    }

    public static Room Create(string name)
    {
        var id = RoomId.CreateUnique();
        return new Room(id, name);
    }

    public void UpdateName(string name)
    {
        if (string.IsNullOrWhiteSpace(name))
        {
            throw new CannotBeEmptyException(nameof(name));
        }

        Name = name;
    }

}
