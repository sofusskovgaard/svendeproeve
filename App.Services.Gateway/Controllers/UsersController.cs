using App.Services.Users.Infrastructure.Grpc;
using App.Services.Users.Infrastructure.Grpc.CommandMessages;
using Microsoft.AspNetCore.Mvc;

namespace App.Services.Gateway.Controllers;

[Route("api/[controller]")]
public class UsersController : ControllerBase
{
    private readonly IUsersGrpcService _usersGrpcService;

    public UsersController(IUsersGrpcService usersGrpcService)
    {
        this._usersGrpcService = usersGrpcService;
    }

    [HttpGet]
    [Route("")]
    public async ValueTask<IActionResult> Index()
    {
        var res = await this._usersGrpcService.GetUserById(new GetUserByIdCommandMessage() { Id = "lmao" });

        return Ok(res);
    }
}