using ErrorOr;
using LogisticsApp.Application.Authentication.Common;
using MediatR;

namespace LogisticsApp.Application.Authentication.Queries.Login;

public record LoginQuery(
    string Email,
    string Password) : IRequest<ErrorOr<AuthenticationResult>>;