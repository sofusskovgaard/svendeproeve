﻿using System.Net.Mime;
using System.Security.Cryptography;
using App.Common.Grpc;
using App.Services.Authentication.Infrastructure.Grpc;
using App.Services.Authentication.Infrastructure.Grpc.CommandMessages;
using App.Services.Authentication.Infrastructure.Grpc.CommandResults;
using App.Services.Gateway.Common;
using App.Services.Gateway.Infrastructure;
using App.Services.Users.Infrastructure.Grpc;
using App.Services.Users.Infrastructure.Grpc.CommandMessages;
using App.Services.Users.Infrastructure.Grpc.CommandResults;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

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
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(LoginGrpcCommandResult))]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(IGrpcCommandResult))]
    public Task<IActionResult> Login([FromBody] LoginModel model)
    {
        return TryAsync(() => _authenticationGrpcService.Login(new LoginGrpcCommandMessage
        {
            Username = model.Username,
            Password = model.Password,
            IP = HttpContext.Connection.RemoteIpAddress?.ToString(),
            UserAgent = HttpContext.Request.Headers.UserAgent
        }));
    }

    /// <summary>
    ///     Register a new user
    /// </summary>
    /// <param name="model"></param>
    /// <returns></returns>
    [HttpPost]
    [Route("register")]
    [ProducesResponseType(StatusCodes.Status202Accepted, Type = typeof(RegisterGrpcCommandResult))]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(IGrpcCommandResult))]
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
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(RefreshTokenGrpcCommandResult))]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(IGrpcCommandResult))]
    public Task<IActionResult> RefreshToken([FromBody] RefreshTokenModel model)
    {
        return TryAsync(() => _authenticationGrpcService.RefreshToken(this.CreateCommandMessage<RefreshTokenGrpcCommandMessage>(message => message.RefreshToken = model.RefreshToken)));
    }

    /// <summary>
    ///     Get currently logged in user
    /// </summary>
    /// <returns></returns>
    [HttpGet]
    [Route("me"), Authorize]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(GetUserByIdGrpcCommandResult))]
    [ProducesResponseType(StatusCodes.Status401Unauthorized, Type = typeof(IGrpcCommandResult))]
    public Task<IActionResult> GetCurrentlyLoggedInUser()
    {
        return TryAsync(() => _usersGrpcService.GetUserById(CreateCommandMessage<GetUserByIdGrpcCommandMessage>(message => message.Id = CurrentUser!.Id)));
    }

    /// <summary>
    ///     Change username for the currently logged in user
    /// </summary>
    /// <param name="model"></param>
    /// <returns></returns>
    [HttpPut]
    [Route("change-username"), Authorize]
    [ProducesResponseType(StatusCodes.Status202Accepted, Type = typeof(ChangeUsernameGrpcCommandResult))]
    [ProducesResponseType(StatusCodes.Status401Unauthorized, Type = typeof(IGrpcCommandResult))]
    public Task<IActionResult> ChangeUsername([FromBody] ChangeUsernameModel model)
    {
        return TryAsync(
            () => _authenticationGrpcService.ChangeUsername(CreateCommandMessage<ChangeUsernameGrpcCommandMessage>(message => message.Username = model.Username)), true);
    }

    /// <summary>
    /// Change email for the currently logged in user
    /// </summary>
    /// <param name="model"></param>
    /// <returns></returns>
    [HttpPut]
    [Route("change-email"), Authorize]
    [ProducesResponseType(StatusCodes.Status202Accepted, Type = typeof(ChangeEmailGrpcCommandResult))]
    [ProducesResponseType(StatusCodes.Status401Unauthorized, Type = typeof(IGrpcCommandResult))]
    public Task<IActionResult> ChangeEmail([FromBody] ChangeEmailModel model)
    {
        return TryAsync(
            () => _authenticationGrpcService.ChangeEmail(CreateCommandMessage<ChangeEmailGrpcCommandMessage>(message => message.Email = model.Email)), true);
    }

    /// <summary>
    ///     Change password for the currently logged in user
    /// </summary>
    /// <param name="model"></param>
    /// <returns></returns>
    [HttpPut]
    [Route("change-password"), Authorize]
    [ProducesResponseType(StatusCodes.Status202Accepted, Type = typeof(ChangePasswordGrpcCommandResult))]
    [ProducesResponseType(StatusCodes.Status401Unauthorized, Type = typeof(IGrpcCommandResult))]
    public Task<IActionResult> ChangePassword([FromBody] ChangePasswordModel model)
    {
        return TryAsync(
            () => _authenticationGrpcService.ChangePassword(CreateCommandMessage<ChangePasswordGrpcCommandMessage>(
                message =>
                {
                    message.Password = model.Password;
                    message.ConfirmPassword = model.ConfirmPassword;
                })), true);
    }

    /// <summary>
    ///     Get sessions for the currently logged in user
    /// </summary>
    /// <returns></returns>
    [HttpGet]
    [Route("sessions"), Authorize]
    [ProducesResponseType(StatusCodes.Status202Accepted, Type = typeof(GetSessionsGrpcCommandResult))]
    [ProducesResponseType(StatusCodes.Status401Unauthorized, Type = typeof(IGrpcCommandResult))]
    public Task<IActionResult> GetSessions()
    {
        return TryAsync(
            () => _authenticationGrpcService.GetSessions(CreateCommandMessage<GetSessionsGrpcCommandMessage>()));
    }

    /// <summary>
    ///     Kill a specific session for the currently logged in user
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [HttpDelete]
    [Route("sessions/{id}"), Authorize]
    [ProducesResponseType(StatusCodes.Status202Accepted, Type = typeof(KillUserSessionsGrpcCommandResult))]
    [ProducesResponseType(StatusCodes.Status401Unauthorized, Type = typeof(IGrpcCommandResult))]
    public Task<IActionResult> KillSessionById(string id)
    {
        return TryAsync(
            () => _authenticationGrpcService.KillSessions(
                CreateCommandMessage<KillUserSessionsGrpcCommandMessage>(message => message.SessionId = new[] { id })), true);
    }

    /// <summary>
    ///     Kill all sessions for the currently logged in user
    /// </summary>
    /// <returns></returns>
    [HttpDelete]
    [Route("sessions/all"), Authorize]
    [ProducesResponseType(StatusCodes.Status202Accepted, Type = typeof(KillUserSessionsGrpcCommandResult))]
    [ProducesResponseType(StatusCodes.Status401Unauthorized, Type = typeof(IGrpcCommandResult))]
    public Task<IActionResult> KillAllSessions()
    {
        return TryAsync(
            () => _authenticationGrpcService.KillSessions(CreateCommandMessage<KillUserSessionsGrpcCommandMessage>()), true);
    }

    /// <summary>
    ///     Get the current public ecdsa key for validation of access tokens
    /// </summary>
    /// <returns></returns>
    [HttpGet]
    [Route("public-key")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> PublicKey()
    {
        var response =
            await this._authenticationGrpcService.PublicKey(
                this.CreateCommandMessage<GetPublicKeyGrpcCommandMessage>());

        var ecdsa = ECDsa.Create();
        ecdsa.ImportSubjectPublicKeyInfo(new ReadOnlySpan<byte>(Convert.FromBase64String(response.PublicKey)), out _);

        var jwk = JsonWebKeyConverter.ConvertFromECDsaSecurityKey(new ECDsaSecurityKey(ecdsa)
            { KeyId = response.KeyId });

        jwk.KeyOps.Add("verify");
        jwk.Use = "sig";
        jwk.Alg = "ES256";

        return this.Ok(new
        {
            alg = jwk.Alg,
            crv = jwk.Crv,
            kid = jwk.Kid,
            kty = jwk.Kty,
            x = jwk.X,
            y = jwk.Y,
            keyOps = jwk.KeyOps,
            use = jwk.Use
        });
    }
}
