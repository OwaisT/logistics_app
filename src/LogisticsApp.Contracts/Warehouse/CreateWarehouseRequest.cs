namespace LogisticsApp.Contracts.Warehouse;

public record CreateWarehouseRequest(
    string Name,
    string Street,
    string Area,
    string City,
    string Postcode,
    string Country
);