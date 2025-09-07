using LogisticsApp.Domain.Common.Models;

namespace LogisticsApp.Domain.Aggregates.Warehouse.ValueObjects;

public sealed class WarehouseId : ValueObject
{
    private string CountryCode { get; }
    private string CityCode { get; }
    private string AreaCode { get; }
    private string UniqueNumber { get; }
    public string WarehouseCode => $"{CountryCode}-{CityCode}-{AreaCode}-{UniqueNumber}";

    private WarehouseId(string countryCode, string cityCode, string areaCode, string uniqueNumber)
    {
        CountryCode = countryCode;
        CityCode = cityCode;
        AreaCode = areaCode;
        UniqueNumber = uniqueNumber;
    }

    public static WarehouseId Create(string countryCode, string cityCode, string areaCode, string uniqueNumber)
    {
        return new WarehouseId(countryCode, cityCode, areaCode, uniqueNumber);
    }
    public override IEnumerable<object> GetEqualityComponents()
    {
        yield return WarehouseCode;
    }
}
