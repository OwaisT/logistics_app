namespace LogisticsApp.Contracts.Authentication;

public record AuthenticationResponse(
    Guid Id,
    string FirstName,
    string LastName,
    string Email,
    IReadOnlyList<string> Roles,
    string Token);
