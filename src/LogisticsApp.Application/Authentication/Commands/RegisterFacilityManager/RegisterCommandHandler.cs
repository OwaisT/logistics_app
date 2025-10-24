using ErrorOr;
using LogisticsApp.Application.Authentication.Common;
using LogisticsApp.Application.Common.Interfaces.Authentication;
using LogisticsApp.Application.Common.Interfaces.Persistence;
using MediatR;
using LogisticsApp.Domain.Shared.Aggregates.User.Services;

namespace LogisticsApp.Application.Authentication.Commands.RegisterFacilityManager;

public class RegisterFacilityManagerCommandHandler(
    IJwtTokenGenerator _jwtTokenGenerator,
    IUserRepository _userRepository,
    IUserUniquenessChecker _userUniquenessChecker) : 
    IRequestHandler<RegisterFacilityManagerCommand, ErrorOr<AuthenticationResult>>
{
    public async Task<ErrorOr<AuthenticationResult>> Handle(RegisterFacilityManagerCommand command, CancellationToken cancellationToken)
    {
        await Task.CompletedTask;
        // Create user
        var userResult = CreateFacilityManager.Execute(_userUniquenessChecker, command.FirstName, command.LastName, command.Email, command.Password);
        if (userResult.IsError)
        {
            return userResult.Errors;
        }

        var user = userResult.Value;
        _userRepository.Add(user);

        // Create JWT token
        var token = _jwtTokenGenerator.GenerateToken(user);

        return new AuthenticationResult(user, token);
    }
}