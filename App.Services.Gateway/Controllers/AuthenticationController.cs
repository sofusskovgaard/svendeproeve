using System.Net.Mime;
using App.Services.Authentication.Infrastructure.Grpc;
using App.Services.Authentication.Infrastructure.Grpc.CommandMessages;
using App.Services.Gateway.Infrastructure;
using App.Services.Users.Infrastructure.Grpc;
using App.Services.Users.Infrastructure.Grpc.CommandMessages;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace App.Services.Gateway.Controllers;

[Route("api/auth")]
[Produces(MediaTypeNames.Application.Json)]
[Consumes(MediaTypeNames.Application.Json)]
public class AuthenticationController : ApiController
{
    private readonly IAuthenticationGrpcService _authenticationGrpcService;

    private readonly IUsersGrpcService _usersGrpcService;

    public AuthenticationController(IAuthenticationGrpcService authenticationGrpcService,
        IUsersGrpcService usersGrpcService)
    {
        _authenticationGrpcService = authenticationGrpcService;
        _usersGrpcService = usersGrpcService;
    }

    /// <summary>
    ///     Login as an existing user
    /// </summary>
    /// <param name="model"></param>
    /// <returns></returns>
    [HttpPost]
    [Route("login")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public Task<IActionResult> Login([FromBody] LoginModel model)
    {
        return TryAsync(() => _authenticationGrpcService.Login(new LoginGrpcCommandMessage
        {
            Username = model.Username,
            Password = model.Password
        }));
    }

    /// <summary>
    ///     Register a new user
    /// </summary>
    /// <param name="model"></param>
    /// <returns></returns>
    [HttpPost]
    [Route("register")]
    [ProducesResponseType(StatusCodes.Status202Accepted)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public Task<IActionResult> Register([FromBody] RegisterModel model)
    {
        return TryAsync(() => _authenticationGrpcService.Register(new RegisterGrpcCommandMessage
        {
            Firstname = model.Firstname,
            Lastname = model.Lastname,
            Username = model.Username,
            Email = model.Email,
            Password = model.Password,
            ConfirmPassword = model.ConfirmPassword
        }), true);
    }

    /// <summary>
    ///     Get a new access token
    /// </summary>
    /// <param name="model"></param>
    /// <returns></returns>
    [HttpPost]
    [Route("refresh")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public Task<IActionResult> RefreshToken([FromBody] RefreshTokenModel model)
    {
        return TryAsync(() => _authenticationGrpcService.RefreshToken(new RefreshTokenGrpcCommandMessage { RefreshToken = model.RefreshToken }));
    }

    /// <summary>
    ///     Get currently logged in user
    /// </summary>
    /// <returns></returns>
    [HttpGet]
    [Route("me"), Authorize]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public Task<IActionResult> GetCurrentlyLoggedInUser()
    {
        return TryAsync(() => _usersGrpcService.GetUserById(new GetUserByIdGrpcCommandMessage(CurrentUser.Id)));
    }

    /// <summary>
    ///     Change username for the currently logged in user
    /// </summary>
    /// <param name="model"></param>
    /// <returns></returns>
    [HttpPut]
    [Route("change-username"), Authorize]
    [ProducesResponseType(StatusCodes.Status202Accepted)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public Task<IActionResult> ChangeUsername([FromBody] ChangeUsernameModel model)
    {
        return TryAsync(
            () => _authenticationGrpcService.ChangeUsername(new ChangeUsernameGrpcCommandMessage
                { UserId = CurrentUser.Id, Username = model.Username }), true);
    }

    /// <summary>
    /// Change email for the currently logged in user
    /// </summary>
    /// <param name="model"></param>
    /// <returns></returns>
    [HttpPut]
    [Route("change-email"), Authorize]
    [ProducesResponseType(StatusCodes.Status202Accepted)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public Task<IActionResult> ChangeEmail([FromBody] ChangeEmailModel model)
    {
        return TryAsync(
            () => _authenticationGrpcService.ChangeEmail(new ChangeEmailGrpCommandMessage
                { UserId = CurrentUser.Id, Email = model.Email }), true);
    }

    /// <summary>
    ///     Change password for the currently logged in user
    /// </summary>
    /// <param name="model"></param>
    /// <returns></returns>
    [HttpPut]
    [Route("change-password"), Authorize]
    [ProducesResponseType(StatusCodes.Status202Accepted)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public Task<IActionResult> ChangePassword([FromBody] ChangePasswordModel model)
    {
        return TryAsync(
            () => _authenticationGrpcService.ChangePassword(new ChangePasswordGrpcCommandMessage
                { UserId = CurrentUser.Id, Password = model.Password, ConfirmPassword = model.ConfirmPassword }), true);
    }

    /// <summary>
    ///     Kill a specific session for the currently logged in user
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [HttpDelete]
    [Route("kill-session/{id}"), Authorize]
    [ProducesResponseType(StatusCodes.Status202Accepted)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public Task<IActionResult> KillSessionById(string id)
    {
        return TryAsync(
            () => _authenticationGrpcService.KillSessions(new KillUserSessionsGrpcCommandMessage() { UserId = this.CurrentUser.Id, SessionId = new[] { id }}), true);
    }

    /// <summary>
    ///     Kill all sessions for the currently logged in user
    /// </summary>
    /// <returns></returns>
    [HttpDelete]
    [Route("kill-session/all"), Authorize]
    [ProducesResponseType(StatusCodes.Status202Accepted)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public Task<IActionResult> KillAllSessions()
    {
        return TryAsync(
            () => _authenticationGrpcService.KillSessions(new KillUserSessionsGrpcCommandMessage() { UserId = this.CurrentUser.Id }), true);
    }
}

public record LoginModel(string Username, string Password);

public record RegisterModel(string Firstname, string Lastname, string Username, string Email, string Password,
    string ConfirmPassword);

public record RefreshTokenModel(string RefreshToken);

public record ChangeUsernameModel(string Username);

public record ChangeEmailModel(string Email);

public record ChangePasswordModel(string Password, string ConfirmPassword);