using System.Net.Mime;
using App.Common.Grpc;
using App.Services.Gateway.Infrastructure;
using App.Services.Users.Infrastructure.Grpc;
using App.Services.Users.Infrastructure.Grpc.CommandMessages;
using App.Services.Users.Infrastructure.Grpc.CommandResults;
using Microsoft.AspNetCore.Authorization;
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
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(GetUsersGrpcCommandResult))]
    public Task<IActionResult> GetAllUsers()
    {
        return TryAsync(() => _usersGrpcService.GetUsers(CreateCommandMessage<GetUsersGrpcCommandMessage>()));
    }

    /// <summary>
    /// Get user by id
    /// </summary>
    /// <param name="id">id of the user</param>
    /// <returns></returns>
    [HttpGet]
    [Route("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(GetUserByIdGrpcCommandResult))]
    [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(IGrpcCommandResult))]
    public Task<IActionResult> GetUserById(string id)
    {
        return TryAsync(() => _usersGrpcService.GetUserById(CreateCommandMessage<GetUserByIdGrpcCommandMessage>(message => message.Id = id)));
    }
}
