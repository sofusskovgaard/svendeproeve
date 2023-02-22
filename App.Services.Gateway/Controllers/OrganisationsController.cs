using App.Services.Organizations.Infrastructure.Grpc;
using App.Services.Organizations.Infrastructure.Grpc.CommandMessages;
using Microsoft.AspNetCore.Mvc;

namespace App.Services.Gateway.Controllers;

[Route("api/[controller]")]
public class OrganisationsController : ControllerBase
{
    private readonly IOrganizationsGrpcService _organizationsGrpcService;
    public OrganisationsController(IOrganizationsGrpcService organizationsGrpcService)
    {
        this._organizationsGrpcService = organizationsGrpcService;
    }

    [HttpGet]
    [Route("")]
    public async ValueTask<IActionResult> Index()
    {
        var res = await this._organizationsGrpcService.GetOrganizationById(new GetOrganizationByIdCommandMessage() { Id = "lmao2" });
        return Ok(res);
    }
}

