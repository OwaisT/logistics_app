namespace LogisticsApp.Contracts.Authentication;

public record RegisterFacilityWorkerRequest(
    string FirstName,
    string LastName,
    string Email,
    string Password);
