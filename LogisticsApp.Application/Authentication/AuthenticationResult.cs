using LogisticsApp.Domain.Entities;

namespace LogisticsApp.Application.Authentication;

public record AuthenticationResult(
    User User,
    string Token);