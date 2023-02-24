using App.Services.Gateway.Infrastructure;
using App.Services.Users.Infrastructure.Grpc;
using App.Services.Users.Infrastructure.Grpc.CommandMessages;
using Microsoft.AspNetCore.Mvc;

namespace App.Services.Gateway.Controllers;

[Route("api/[controller]")]
public class UsersController : ApiController
{
    private readonly IUsersGrpcService _usersGrpcService;

    public UsersController(IUsersGrpcService usersGrpcService)
    {
        _usersGrpcService = usersGrpcService;
    }

    [HttpGet]
    [Route("{id}")]
    public Task<IActionResult> GetUserById(string id)
    {
        return TryAsync(() => _usersGrpcService.GetUserById(new GetUserByIdCommandMessage { Id = id }));
    }

    [HttpPost]
    [Route("")]
    public Task<IActionResult> CreateUser([FromBody] CreateUserModel model)
    {
        return TryAsync(() =>
        {
            var command = new CreateUserCommandMessage
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

    [HttpGet]
    [Route("test")]
    public Task<IActionResult> Test()
    {
        return TryAsync(() => _usersGrpcService.Test());
    }
}

public record CreateUserModel(string Firstname, string Lastname, string Username, string Email, string Password);