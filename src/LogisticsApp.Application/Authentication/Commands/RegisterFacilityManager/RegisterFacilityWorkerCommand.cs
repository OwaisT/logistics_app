using ErrorOr;
using LogisticsApp.Application.Authentication.Common;
using MediatR;

namespace LogisticsApp.Application.Authentication.Commands.RegisterFacilityManager;

public record RegisterFacilityManagerCommand(
    string FirstName,
    string LastName,
    string Email,
    string Password) : IRequest<ErrorOr<AuthenticationResult>>;