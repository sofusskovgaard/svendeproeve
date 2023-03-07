using System.Net.Mime;
using System.Xml.Linq;
using App.Common.Grpc;
using App.Services.Gateway.Common;
using App.Services.Gateway.Infrastructure;
using App.Services.Teams.Common.Dtos;
using App.Services.Teams.Infrastructure.Grpc;
using App.Services.Teams.Infrastructure.Grpc.CommandMessages;
using App.Services.Teams.Infrastructure.Grpc.CommandResults;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace App.Services.Gateway.Controllers;

[Route("api/[controller]")]
[Produces(MediaTypeNames.Application.Json)]
[Consumes(MediaTypeNames.Application.Json)]
public class TeamsController : ApiController
{
    private readonly ITeamsGrpcService _teamsGrpcService;

    public TeamsController(ITeamsGrpcService teamsGrpcService)
    {
        _teamsGrpcService = teamsGrpcService;
    }

    /// <summary>
    ///     Get all teams
    /// </summary>
    /// <param name="searchText">if specified will return teams searched in name and bio</param>
    /// <param name="gameId">if specified will return teams assigned with this game</param>
    /// <param name="organizationId">if specified will return teams in this organization</param>
    /// <param name="memberId">if specified will return teams with this member</param>
    /// <param name="managerId">if specified will return teams with this manager</param>
    /// <returns></returns>
    [HttpGet]
    [Route("")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(GetAllTeamsGrpcCommandResult))]
    public Task<IActionResult> GetAllTeams(string? searchText = null, string? gameId = null, string? organizationId = null, string? memberId = null, string? managerId = null)
    {
        return TryAsync(() => _teamsGrpcService.GetAllTeams(CreateCommandMessage<GetAllTeamsGrpcCommandMessage>(
            message =>
            {
                message.SearchText = searchText;
                message.GameId = gameId;
                message.OrganizationId = organizationId;
                message.MemberId = memberId;
                message.ManagerId = managerId;
            })));
    }

    /// <summary>
    ///     Get team by id or name
    /// </summary>
    /// <param name="id">id or name of team</param>
    /// <returns></returns>
    [HttpGet]
    [Route("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(GetTeamByIdGrpcCommandResult))]
    [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(IGrpcCommandResult))]
    public Task<IActionResult> GetTeamById(string id)
    {
        return TryAsync(() => _teamsGrpcService.GetTeamById(CreateCommandMessage<GetTeamByIdGrpcCommandMessage>(message => message.Id = id)));
    }

    /// <summary>
    ///     Create a team
    /// </summary>
    /// <param name="model"></param>
    /// <returns></returns>
    [HttpPost]
    [Route(""), Authorize]
    [ProducesResponseType(StatusCodes.Status202Accepted, Type = typeof(CreateTeamGrpcCommandResult))]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(IGrpcCommandResult))]
    [ProducesResponseType(StatusCodes.Status401Unauthorized, Type = typeof(IGrpcCommandResult))]
    public Task<IActionResult> CreateTeam([FromBody] CreateTeamModel model)
    {
        return TryAsync(() => _teamsGrpcService.CreateTeam(CreateCommandMessage<CreateTeamGrpcCommandMessage>(message =>
        {
            message.Name = model.Name;
            message.Bio = model.Bio;
            message.ProfilePicturePath = model.ProfilePicturePath;
            message.CoverPicturePath = model.CoverPicturePath;
            message.GameId = model.GameId;
            message.MembersId = model.MembersId;
            message.ManagerId = model.ManagerId;
            message.OrganizationId = model.OrganizationId;
        })), true);
    }

    /// <summary>
    ///     Update a team by id
    /// </summary>
    /// <param name="id"></param>
    /// <param name="model"></param>
    /// <returns></returns>
    [HttpPut]
    [Route("{id}"), Authorize]
    [ProducesResponseType(StatusCodes.Status202Accepted, Type = typeof(UpdateTeamGrpcCommandResult))]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(IGrpcCommandResult))]
    [ProducesResponseType(StatusCodes.Status401Unauthorized, Type = typeof(IGrpcCommandResult))]
    [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(IGrpcCommandResult))]
    public Task<IActionResult> UpdateTeam(string id, [FromBody] UpdateTeamModel model)
    {
        return TryAsync(() => _teamsGrpcService.UpdateTeam(CreateCommandMessage<UpdateTeamGrpcCommandMessage>(message =>
        {
            message.TeamId = id;
            message.TeamDto = new UpdateTeamDto
            {
                Name = model.Name,
                Bio = model.Bio,
                ProfilePicturePath = model.ProfilePicturePath,
                CoverPicturePath = model.CoverPicturePath
            };
        })), true);
    }

    /// <summary>
    ///     Delete a team by id
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [HttpDelete]
    [Route("{id}"), Authorize]
    [ProducesResponseType(StatusCodes.Status202Accepted, Type = typeof(DeleteTeamByIdGrpcCommandResult))]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(IGrpcCommandResult))]
    [ProducesResponseType(StatusCodes.Status401Unauthorized, Type = typeof(IGrpcCommandResult))]
    [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(IGrpcCommandResult))]
    public Task<IActionResult> DeleteTeamById(string id)
    {
        return TryAsync(() => _teamsGrpcService.DeleteTeamById(CreateCommandMessage<DeleteTeamByIdGrpcCommandMessage>(message => message.Id = id)), true);
    }
}