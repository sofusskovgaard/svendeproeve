using System.Net.Mime;
using App.Services.Gateway.Infrastructure;
using App.Services.Users.Infrastructure.Grpc;
using App.Services.Users.Infrastructure.Grpc.CommandMessages;
using Microsoft.AspNetCore.Mvc;

namespace App.Services.Gateway.Controllers;

[Route("api/[controller]")]
[Produces(MediaTypeNames.Application.Json)]
[Consumes(MediaTypeNames.Application.Json)]
public class UsersController : ApiController
{
    private readonly IUsersGrpcService _usersGrpcService;

    public UsersController(IUsersGrpcService usersGrpcService)
    {
        _usersGrpcService = usersGrpcService;
    }

    /// <summary>
    /// Get all users
    /// </summary>
    /// <param name="id">id of the user</param>
    /// <returns></returns>
    [HttpGet]
    [Route("")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public Task<IActionResult> GetAllUsers()
    {
        return TryAsync(() => _usersGrpcService.GetUsers(new GetUsersGrpcCommandMessage()));
    }

    /// <summary>
    /// Get user by id
    /// </summary>
    /// <param name="id">id of the user</param>
    /// <returns></returns>
    [HttpGet]
    [Route("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public Task<IActionResult> GetUserById(string id)
    {
        return TryAsync(() => _usersGrpcService.GetUserById(new GetUserByIdGrpcCommandMessage { Id = id }));
    }

    /// <summary>
    /// Get currently logged in user
    /// </summary>
    /// <returns></returns>
    /// <exception cref="NotImplementedException"></exception>
    [HttpGet]
    [Route("me")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public Task<IActionResult> GetCurrentlyLoggedInUser()
    {
        throw new NotImplementedException();
    }

    /// <summary>
    /// Create a user
    /// </summary>
    /// <param name="model">the data required to create user</param>
    /// <returns></returns>
    [HttpPost]
    [Route("")]
    [ProducesResponseType(StatusCodes.Status202Accepted)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public Task<IActionResult> CreateUser([FromBody] CreateUserModel model)
    {
        return TryAsync(() =>
        {
            var command = new CreateUserGrpcCommandMessage
            {
                Firstname = model.Firstname,
                Lastname = model.Lastname,
                Username = model.Username,
                Email = model.Email,
                Password = model.Password
            };

            return _usersGrpcService.CreateUser(command);
        });
    }

    /// <summary>
    /// Get teams a specific user is part of
    /// </summary>
    /// <param name="id">id of the user</param>
    /// <returns></returns>
    /// <exception cref="NotImplementedException"></exception>
    [HttpGet]
    [Route("{id}/teams")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public Task<IActionResult> GetTeamsForUser(string id)
    {
        return TryAsync(() => _usersGrpcService.GetUsersInTeam(new GetUsersInTeamGrpcCommandMessage() { TeamId = id }));
    }

    /// <summary>
    /// Get organizations a specific user is part of
    /// </summary>
    /// <param name="id">id of the user</param>
    /// <returns></returns>
    /// <exception cref="NotImplementedException"></exception>
    [HttpGet]
    [Route("{id}/organizations")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public Task<IActionResult> GetOrganizationsForUser(string id)
    {
        return TryAsync(() => _usersGrpcService.GetUsersInOrganization(new GetUsersInOrganizationGrpcCommandMessage() { OrganizatioId = id }));
    }
}

/// <summary>
/// Data required to create a new user
/// </summary>
/// <param name="Firstname">The firstname</param>
/// <param name="Lastname">The lastname</param>
/// <param name="Username">The username, is unique</param>
/// <param name="Email">The email, is unique</param>
/// <param name="Password">The password</param>
public record CreateUserModel(string Firstname, string Lastname, string Username, string Email, string Password);