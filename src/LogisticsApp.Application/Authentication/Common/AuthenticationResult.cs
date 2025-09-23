using LogisticsApp.Domain.Shared.Aggregates.User;

namespace LogisticsApp.Application.Authentication.Common;

public record AuthenticationResult(
    User User,
    string Token);