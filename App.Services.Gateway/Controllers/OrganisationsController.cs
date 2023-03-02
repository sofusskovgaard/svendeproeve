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
    public Task<IActionResult> GetOrganizationById(string id)
    {
        return TryAsync(() =>
        {
            return _organizationsGrpcService.GetOrganizationById(new GetOrganizationByIdGrpcCommandMessage { Id = id });
        });
    }

    [HttpGet]
    [Route("{name}/name")]
    public Task<IActionResult> GetOrganizationByName(string name)
    {
        return TryAsync(() =>
        {
            return _organizationsGrpcService.GetOrganizationsByName(new GetOrganizationsByNameGrpcCommandMessage { Name = name });
        });
    }

    [HttpGet]
    [Route("{address}/address")]
    public Task<IActionResult> GetOrganizationsByAddress(string address)
    {
        return TryAsync(() =>
        {
            return _organizationsGrpcService.GetOrganizationsByAddress(new GetOrganizationsByAddressGrpcCommandMessage { Address = address });
        });
    }

    [HttpGet]
    [Route("")]
    public Task<IActionResult> GetOrganizations()
    {
        return TryAsync(() =>
        {
            return _organizationsGrpcService.GetOrganizations(new GetOrganizationsGrpcCommandMessage { });
        });
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
            var command = new CreateOrganizationGrpcCommandMessage
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

    /// <summary>
    /// Update organizations information
    /// </summary>
    /// <param name="id"></param>
    /// <param name="model"></param>
    /// <returns></returns>
    [HttpPut]
    [Route("{id}")]
    public Task<IActionResult> UpdateOrganization(string id, [FromBody] UpdateOrganizaitonModel model)
    {
        return TryAsync(() =>
        {
            var command = new UpdateOrganizationGrpcCommandMessage
            {
                Id = id,
                Address = model.Address,
                Bio = model.Bio,
                CoverPicture = model.CoverPicture,
                Name = model.Name,
                ProfilePicture = model.ProfilePicture,
            };

            return _organizationsGrpcService.UpdateOrganization(command);
        });
    }

    /// <summary>
    /// Deleltes organization
    /// </summary>
    /// <param name="id">id of the organization to be deleted</param>
    /// <returns></returns>
    [HttpDelete]
    [Route("{id}")]
    public Task<IActionResult> DeleteOrganization(string id)
    {
        return TryAsync(() =>
        {
            var command = new DeleteOrganizationGrpcCommandMessage
            {
                Id = id
            };

            return _organizationsGrpcService.DeleteOrganization(command);
        });
    }
}

public record CreateOrganizationModel(string Name, string Bio, string ProfilePicture, string CoverPicture, string Address);
public record UpdateOrganizaitonModel(string Name, string Bio, string ProfilePicture, string CoverPicture, string Address);