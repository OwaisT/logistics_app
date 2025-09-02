using LogisticsApp.Domain.Entities;

namespace LogisticsApp.Application.Authentication.Common;

public record AuthenticationResult(
    User User,
    string Token);