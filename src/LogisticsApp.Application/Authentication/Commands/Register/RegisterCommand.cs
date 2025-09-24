using ErrorOr;
using LogisticsApp.Application.Authentication.Common;
using MediatR;

namespace LogisticsApp.Application.Authentication.Commands.Register;

public record RegisterCommand(
    string FirstName,
    string LastName,
    string Email,
    string Password,
    List<string> Roles) : IRequest<ErrorOr<AuthenticationResult>>;