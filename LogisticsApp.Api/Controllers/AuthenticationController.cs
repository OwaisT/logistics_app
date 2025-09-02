using Microsoft.AspNetCore.Mvc;
using LogisticsApp.Contracts.Authentication;
using LogisticsApp.Application.Authentication;
using ErrorOr;

namespace LogisticsApp.Api.Controllers;

[Route("auth")]
public class AuthenticationController : ApiController
{
    private readonly IAuthenticationService _authenticationService;

    public AuthenticationController(IAuthenticationService authenticationService)
    {
        _authenticationService = authenticationService;
    }

    [HttpPost("register")]
    public IActionResult Register([FromBody] RegisterRequest request)
    {
        ErrorOr<AuthenticationResult> authResult = _authenticationService.Register(
            request.FirstName,
            request.LastName,
            request.Email,
            request.Password);
        return authResult.Match(
            authResult => Ok(MapAuthResultToResponse(authResult)),
            errors => Problem(errors));
    }

    [HttpPost("login")]
    public IActionResult Login([FromBody] LoginRequest request)
    {
        var authResult = _authenticationService.Login(request.Email, request.Password);
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