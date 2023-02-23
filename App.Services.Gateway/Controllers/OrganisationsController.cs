using App.Services.Organizations.Data.Entities;
using App.Services.Organizations.Infrastructure.Grpc;
using App.Services.Organizations.Infrastructure.Grpc.CommandMessages;
using App.Services.Organizations.Infrastructure.Grpc.CommandResults;
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
    [Route("GetOrganizationById")]
    public async ValueTask<IActionResult> GetOrganizationById(string id)
    {
        var res = await this._organizationsGrpcService.GetOrganizationById(new GetOrganizationByIdCommandMessage() { Id = id });
        return Ok(res);
    }

    [HttpGet]
    [Route("GetOrganizationByName")]
    public async ValueTask<IActionResult> GetOrganizationByName(string name)
    {
        var res = await this._organizationsGrpcService.GetOrganizationsByName(new GetOrganizationsByNameCommandMessage() { Name = name});
        return Ok(res);
    }

    [HttpGet]
    [Route("GetOrganizationsByAddress")]
    public async ValueTask<IActionResult> GetOrganizationsByAddress(string address)
    {
        var res = await this._organizationsGrpcService.GetOrganizationsByAddress(new GetOrganizationsByAddressCommandMessage() { Address = address });
        return Ok(res);
    }

    [HttpPost]
    [Route("CreateOrganization")]
    public async ValueTask<IActionResult> CreateOrganization(string name)
    {
        var res = await this._organizationsGrpcService.CreateOrganization(new CreateOrganizationCommandMessage() { Name = name });
        return Accepted(res);
    }
}

