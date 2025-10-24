using Microsoft.AspNetCore.Mvc;
using LogisticsApp.Contracts.Authentication;
using ErrorOr;
using MediatR;
using LogisticsApp.Application.Authentication.Common;
using LogisticsApp.Application.Authentication.Queries.Login;
using MapsterMapper;
using Microsoft.AspNetCore.Authorization;
using LogisticsApp.Application.Authentication.Commands.RegisterFacilityWorker;
using LogisticsApp.Application.Authentication.Commands.RegisterFacilityManager;

namespace LogisticsApp.Api.Controllers;

[Route("auth")]
public class AuthenticationController(ISender _mediator, IMapper _mapper) : ApiController
{
    [Authorize(Roles = "BusinessManager,FacilityManager")]
    [HttpPost("register/FacilityWorker")]
    public async Task<IActionResult> Register(RegisterFacilityWorkerRequest request)
    {
        var Command = _mapper.Map<RegisterFacilityWorkerCommand>(request);
        ErrorOr<AuthenticationResult> authResult = await _mediator.Send(Command);
        return authResult.Match(
            authResult => Ok(_mapper.Map<AuthenticationResponse>(authResult)),
            Problem);
    }

    [Authorize(Roles = "BusinessManager")]
    [HttpPost("register/FacilityManager")]
    public async Task<IActionResult> Register(RegisterFacilityManagerRequest request)
    {
        var Command = _mapper.Map<RegisterFacilityManagerCommand>(request);
        ErrorOr<AuthenticationResult> authResult = await _mediator.Send(Command);
        return authResult.Match(
            authResult => Ok(_mapper.Map<AuthenticationResponse>(authResult)),
            Problem);
    }

    [HttpPost("login")]
    [AllowAnonymous]
    public async Task<IActionResult> Login(LoginRequest request)
    {
        var query = _mapper.Map<LoginQuery>(request);
        var authResult = await _mediator.Send(query);
        return authResult.Match(
            authResult => Ok(_mapper.Map<AuthenticationResponse>(authResult)),
            Problem);
    }
}