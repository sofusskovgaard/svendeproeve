using App.Services.Departments.Infrastructure.Grpc.CommandResults;
using App.Services.Gateway.Common;
using App.Services.Gateway.Infrastructure;
using App.Services.Organizations.Infrastructure.Grpc;
using App.Services.Organizations.Infrastructure.Grpc.CommandMessages;
using Microsoft.AspNetCore.Mvc;
using System.Net.Mime;
using App.Services.Organizations.Infrastructure.Grpc.CommandResults;
using App.Common.Grpc;
using Microsoft.AspNetCore.Authorization;

namespace App.Services.Gateway.Controllers;

[Route("api/[controller]")]
[Produces(MediaTypeNames.Application.Json)]
[Consumes(MediaTypeNames.Application.Json)]
public class OrganizationsController : ApiController
{
    private readonly IOrganizationsGrpcService _organizationsGrpcService;

    public OrganizationsController(IOrganizationsGrpcService organizationsGrpcService)
    {
        _organizationsGrpcService = organizationsGrpcService;
    }

    /// <summary>
    ///     Get all organizations
    /// </summary>
    /// <param name="searchText">if specified will search organizations for this</param>
    /// <param name="memberId">if specified will return organizations for this member</param>
    /// <param name="departmentId">if specified will return organizations in this department</param>
    /// <returns></returns>
    [HttpGet]
    [Route("")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(GetOrganizationsGrpcCommandMessage))]
    public Task<IActionResult> GetOrganizations(string? searchText = null, string? memberId = null, string? departmentId = null)
    {
        return TryAsync(() => _organizationsGrpcService.GetOrganizations(CreateCommandMessage<GetOrganizationsGrpcCommandMessage>(
            message =>
            {
                message.SearchText = searchText;
                message.MemberId = memberId;
                message.DepartmentId = departmentId;
            })));
    }

    /// <summary>
    ///     Get organization by id or name
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [HttpGet]
    [Route("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(GetOrganizationByIdGrpcCommandMessage))]
    [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(IGrpcCommandResult))]
    public Task<IActionResult> GetOrganizationById(string id)
    {
        return TryAsync(() => _organizationsGrpcService.GetOrganizationById(
            CreateCommandMessage<GetOrganizationByIdGrpcCommandMessage>(message => message.Id = id)));
    }

    /// <summary>
    ///     Create an organization
    /// </summary>
    /// <param name="model"></param>
    /// <returns></returns>
    /// <response code="202">Returns if it has accepted the request</response>
    [HttpPost]
    [Route(""), Authorize]
    [ProducesResponseType(StatusCodes.Status202Accepted, Type = typeof(CreateOrganizationGrpcCommandResult))]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(IGrpcCommandResult))]
    [ProducesResponseType(StatusCodes.Status401Unauthorized, Type = typeof(IGrpcCommandResult))]
    public Task<IActionResult> CreateOrganization([FromBody] CreateOrganizationModel model)
    {
        return TryAsync(() => _organizationsGrpcService.CreateOrganization(CreateCommandMessage<CreateOrganizationGrpcCommandMessage>(
            message =>
            {
                message.Name = model.Name;
                message.Address = model.Address;
                message.Bio = model.Bio;
                message.CoverPicture = model.CoverPicture;
                message.ProfilePicture = model.ProfilePicture;
                message.DepartmentId = model.DepartmentId;
            })));
    }

    /// <summary>
    ///     Update an organization
    /// </summary>
    /// <param name="id"></param>
    /// <param name="model"></param>
    /// <returns></returns>
    [HttpPut]
    [Route("{id}"), Authorize]
    [ProducesResponseType(StatusCodes.Status202Accepted, Type = typeof(UpdateOrganizationGrpcCommandResult))]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(IGrpcCommandResult))]
    [ProducesResponseType(StatusCodes.Status401Unauthorized, Type = typeof(IGrpcCommandResult))]
    public Task<IActionResult> UpdateOrganization(string id, [FromBody] UpdateOrganizaitonModel model)
    {
        return TryAsync(() => _organizationsGrpcService.UpdateOrganization(CreateCommandMessage<UpdateOrganizationGrpcCommandMessage>(
            message =>
            {
                message.Id = id;
                message.Address = model.Address;
                message.Bio = model.Bio;
                message.CoverPicture = model.CoverPicture;
                message.Name = model.Name;
                message.ProfilePicture = model.ProfilePicture;
                message.DepartmentId = model.DepartmentId;
            })));
    }

    /// <summary>
    ///     Delete an organization by id
    /// </summary>
    /// <param name="id">id of the organization to be deleted</param>
    /// <returns></returns>
    [HttpDelete]
    [Route("{id}"), Authorize]
    [ProducesResponseType(StatusCodes.Status202Accepted, Type = typeof(DeleteOrganizationGrpcCommandResult))]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(IGrpcCommandResult))]
    [ProducesResponseType(StatusCodes.Status401Unauthorized, Type = typeof(IGrpcCommandResult))]
    public Task<IActionResult> DeleteOrganization(string id)
    {
        return TryAsync(() => _organizationsGrpcService.DeleteOrganization(CreateCommandMessage<DeleteOrganizationGrpcCommandMessage>(message => message.Id = id)));
    }
}