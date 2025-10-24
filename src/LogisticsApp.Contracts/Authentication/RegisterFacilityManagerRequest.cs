namespace LogisticsApp.Contracts.Authentication;

public record RegisterFacilityManagerRequest(
    string FirstName,
    string LastName,
    string Email,
    string Password);
