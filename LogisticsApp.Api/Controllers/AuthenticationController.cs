using Microsoft.AspNetCore.Mvc;
using LogisticsApp.Contracts.Authentication;
using ErrorOr;
using MediatR;
using LogisticsApp.Application.Authentication.Commands.Register;
using LogisticsApp.Application.Authentication.Common;
using LogisticsApp.Application.Authentication.Queries.Login;

namespace LogisticsApp.Api.Controllers;

[Route("auth")]
public class AuthenticationController : ApiController
{
    private readonly IMediator _mediator;
    public AuthenticationController( IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] RegisterRequest request)
    {
        var Command = new RegisterCommand(
            request.FirstName,
            request.LastName,
            request.Email,
            request.Password);
        ErrorOr<AuthenticationResult> authResult = await _mediator.Send(Command);
        return authResult.Match(
            authResult => Ok(MapAuthResultToResponse(authResult)),
            errors => Problem(errors));
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login(LoginRequest request)
    {
        var query = new LoginQuery(
            request.Email,
            request.Password);
        var authResult = await _mediator.Send(query);
        return authResult.Match(
            authResult => Ok(MapAuthResultToResponse(authResult)),
            errors => Problem(errors));
    }

    private static AuthenticationResponse MapAuthResultToResponse(AuthenticationResult authResult)
    {
        return new AuthenticationResponse(
            authResult.User.Id,
            authResult.User.FirstName,
            authResult.User.LastName,
            authResult.User.Email,
            authResult.Token);
    }
}