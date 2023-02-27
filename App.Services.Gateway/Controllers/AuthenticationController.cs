using App.Services.Gateway.Infrastructure;
using Microsoft.AspNetCore.Mvc;
using System.Net.Mime;
using System.Security.Claims;
using App.Services.Authentication.Infrastructure.Grpc;
using App.Services.Authentication.Infrastructure.Grpc.CommandMessages;
using App.Services.Users.Infrastructure.Grpc;
using App.Services.Users.Infrastructure.Grpc.CommandMessages;
using Microsoft.AspNetCore.Authorization;

namespace App.Services.Gateway.Controllers;

[Route("api/auth")]
[Produces(MediaTypeNames.Application.Json)]
[Consumes(MediaTypeNames.Application.Json)]
public class AuthenticationController : ApiController
{
    private readonly IAuthenticationGrpcService _authenticationGrpcService;

    private readonly IUsersGrpcService _usersGrpcService;

    public AuthenticationController(IAuthenticationGrpcService authenticationGrpcService, IUsersGrpcService usersGrpcService)
    {
        _authenticationGrpcService = authenticationGrpcService;
        _usersGrpcService = usersGrpcService;
    }

    /// <summary>
    /// Login as an existing user
    /// </summary>
    /// <param name="model">The data required to login as an existing user</param>
    /// <returns></returns>
    [HttpPost]
    [Route("login")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public Task<IActionResult> Login([FromBody] LoginModel model)
    {
        return this.TryAsync(() => _authenticationGrpcService.Login(new LoginGrpcCommandMessage()
        {
            Username = model.Username,
            Password = model.Password
        }));
    }

    /// <summary>
    /// Register a new user
    /// </summary>
    /// <param name="model">The data required to register a new user</param>
    /// <returns></returns>
    [HttpPost]
    [Route("register")]
    [ProducesResponseType(StatusCodes.Status202Accepted)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public Task<IActionResult> Register([FromBody] RegisterModel model)
    {
        return this.TryAsync(() => _authenticationGrpcService.Register(new RegisterGrpcCommandMessage()
        {
            Firstname = model.Firstname,
            Lastname = model.Lastname,
            Username = model.Username,
            Email = model.Email,
            Password = model.Password
        }), true);
    }

    /// <summary>
    /// Get currently logged in user
    /// </summary>
    /// <returns></returns>
    [HttpGet]
    [Route("me"), Authorize]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public Task<IActionResult> GetCurrentlyLoggedInUser()
    {
        return this.TryAsync(() => _usersGrpcService.GetUserById(new GetUserByIdGrpcCommandMessage(this.User.FindFirst("id")!.Value)));
    }
}

public record LoginModel(string Username, string Password);

public record RegisterModel(string Firstname, string Lastname, string Username, string Email, string Password);