using Microsoft.AspNetCore.Mvc;
using LogisticsApp.Contracts.Authentication;
using ErrorOr;
using MediatR;
using LogisticsApp.Application.Authentication.Commands.Register;
using LogisticsApp.Application.Authentication.Common;
using LogisticsApp.Application.Authentication.Queries.Login;
using MapsterMapper;
using Microsoft.AspNetCore.Authorization;

namespace LogisticsApp.Api.Controllers;

[Route("auth")]
[AllowAnonymous]
public class AuthenticationController : ApiController
{
    private readonly ISender _mediator;
    private readonly IMapper _mapper;
    public AuthenticationController(ISender mediator, IMapper mapper)
    {
        _mediator = mediator;
        _mapper = mapper;
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register(RegisterRequest request)
    {
        var Command = _mapper.Map<RegisterCommand>(request);
        ErrorOr<AuthenticationResult> authResult = await _mediator.Send(Command);
        return authResult.Match(
            authResult => Ok(MapAuthResultToResponse(authResult)),
            Problem);
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login(LoginRequest request)
    {
        var query = _mapper.Map<LoginQuery>(request);
        var authResult = await _mediator.Send(query);
        return authResult.Match(
            authResult => Ok(MapAuthResultToResponse(authResult)),
            Problem);
    }

    private static AuthenticationResponse MapAuthResultToResponse(AuthenticationResult authResult)
    {
        return new AuthenticationResponse(
            authResult.User.Id.Value,
            authResult.User.FirstName,
            authResult.User.LastName,
            authResult.User.Email,
            authResult.User.Roles,
            authResult.Token);
    }
}