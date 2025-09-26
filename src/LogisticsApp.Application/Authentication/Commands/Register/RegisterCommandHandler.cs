using ErrorOr;
using LogisticsApp.Application.Authentication.Common;
using LogisticsApp.Application.Common.Interfaces.Authentication;
using LogisticsApp.Application.Common.Interfaces.Persistence;
using LogisticsApp.Domain.Common.Errors;
using MediatR;
using LogisticsApp.Domain.Shared.Aggregates.User;
using LogisticsApp.Domain.Shared.Aggregates.User.Services;
using LogisticsApp.Application.Authentication.Services;

namespace LogisticsApp.Application.Authentication.Commands.Register;

public class RegisterCommandHandler : 
    IRequestHandler<RegisterCommand, ErrorOr<AuthenticationResult>>
{
    private readonly IJwtTokenGenerator _jwtTokenGenerator;
    private readonly IUserRepository _userRepository;
    private readonly UserFactory _userFactory;
    

    public RegisterCommandHandler(IJwtTokenGenerator jwtTokenGenerator, IUserRepository userRepository)
    {
        _jwtTokenGenerator = jwtTokenGenerator;
        _userRepository = userRepository;
        _userFactory = new UserFactory(new UserUniquenessChecker(_userRepository));
    }

    public async Task<ErrorOr<AuthenticationResult>> Handle(RegisterCommand command, CancellationToken cancellationToken)
    {
        await Task.CompletedTask;
        // Check if user already exists
        if (_userRepository.GetUserByEmail(command.Email) is not null)
        {
            return Errors.User.DuplicateEmail;
        }

        // Create user
        var userResult = _userFactory.Create(command.FirstName, command.LastName, command.Email, command.Password, command.Roles);
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