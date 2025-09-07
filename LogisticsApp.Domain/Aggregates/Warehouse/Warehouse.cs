using LogisticsApp.Domain.Aggregates.Warehouse.Entities;
using LogisticsApp.Domain.Aggregates.Warehouse.ValueObjects;
using LogisticsApp.Domain.Common.Models;

namespace LogisticsApp.Domain.Aggregates.Warehouse;

public sealed class Warehouse : AggregateRoot<WarehouseId>
{
    private readonly List<Room> _rooms = new();
    public string CountryCode { get; private set; }
    public string CityCode { get; private set; }
    public string AreaCode { get; private set; }
    public string UniqueNumber { get; private set; }
    public string Name { get; private set; }
    public string City { get; private set; }
    public string Postcode { get; private set; }
    public string Country { get; private set; }
    public string Area { get; private set; }
    public string Location { get; private set; }
    public DateTime CreatedAt { get; private set; }
    public DateTime? UpdatedAt { get; private set; }
    public bool IsActive { get; private set; }
    public IReadOnlyList<Room> Rooms => _rooms.AsReadOnly();

    private Warehouse(
        WarehouseId id,
        string countryCode,
        string cityCode,
        string areaCode,
        string uniqueNumber,
        string name,
        string city,
        string postcode,
        string country,
        string area,
        string location) : base(id)
    {
        CountryCode = countryCode;
        CityCode = cityCode;
        AreaCode = areaCode;
        UniqueNumber = uniqueNumber;
        Name = name;
        City = city;
        Postcode = postcode;
        Country = country;
        Area = area;
        Location = location;
        CreatedAt = DateTime.UtcNow;
        IsActive = true;
    }

    public static Warehouse Create(
        string countryCode,
        string cityCode,
        string areaCode,
        string uniqueNumber,
        string name,
        string city,
        string postcode,
        string country,
        string area,
        string location)
    {
        var warehouseId = WarehouseId.Create(countryCode, cityCode, areaCode, uniqueNumber);
        return new Warehouse(warehouseId, countryCode, cityCode, areaCode, uniqueNumber, name, city, postcode, country, area, location);
    }
}
