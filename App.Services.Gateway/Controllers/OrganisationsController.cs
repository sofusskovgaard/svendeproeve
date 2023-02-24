using App.Services.Gateway.Infrastructure;
using App.Services.Organizations.Data.Entities;
using App.Services.Organizations.Infrastructure.Grpc;
using App.Services.Organizations.Infrastructure.Grpc.CommandMessages;
using App.Services.Organizations.Infrastructure.Grpc.CommandResults;
using Microsoft.AspNetCore.Mvc;
using ProtoBuf;

namespace App.Services.Gateway.Controllers;

[Route("api/[controller]")]
public class OrganisationsController : ApiController
{
    private readonly IOrganizationsGrpcService _organizationsGrpcService;
    public OrganisationsController(IOrganizationsGrpcService organizationsGrpcService)
    {
        this._organizationsGrpcService = organizationsGrpcService;
    }

    [HttpGet]
    [Route("{id}")]
    public async ValueTask<IActionResult> GetOrganizationById(string id)
    {
        var res = await this._organizationsGrpcService.GetOrganizationById(new GetOrganizationByIdCommandMessage() { Id = id });
        return Ok(res);
    }

    [HttpGet]
    [Route("{name}")]
    public async ValueTask<IActionResult> GetOrganizationByName(string name)
    {
        var res = await this._organizationsGrpcService.GetOrganizationsByName(new GetOrganizationsByNameCommandMessage() { Name = name });
        return Ok(res);
    }

    [HttpGet]
    [Route("{address}")]
    public async ValueTask<IActionResult> GetOrganizationsByAddress(string address)
    {
        var res = await this._organizationsGrpcService.GetOrganizationsByAddress(new GetOrganizationsByAddressCommandMessage() { Address = address });
        return Ok(res);
    }

    /// <summary>
    /// Creates organization from model
    /// </summary>
    /// <param name="model"></param>
    /// <returns></returns>
    /// <response code="202">Returns if it has accepted the request</response>
    [HttpPost]
    [Route("")]
    public Task<IActionResult> CreateOrganization([FromBody] CreateOrganizationModel model)
    {
        return TryAsync(() =>
        {
            var command = new CreateOrganizationCommandMessage
            {
                Name = model.Name,
                Address = model.Address,
                Bio = model.Bio,
                CoverPicture = model.CoverPicture,
                ProfilePicture = model.ProfilePicture,
            };
            return _organizationsGrpcService.CreateOrganization(command);
        });
    }
}

public record CreateOrganizationModel(string Name, string Bio, string ProfilePicture, string CoverPicture, string Address);