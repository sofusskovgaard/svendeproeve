using App.Common.Grpc;
using App.Services.Gateway.Common;
using App.Services.Gateway.Infrastructure;
using App.Services.Tournaments.Common.Dtos;
using App.Services.Tournaments.Infrastructure.Grpc;
using App.Services.Tournaments.Infrastructure.Grpc.CommandMessages;
using App.Services.Tournaments.Infrastructure.Grpc.CommandResults;
using Microsoft.AspNetCore.Mvc;
using System.Xml.Linq;
using Microsoft.AspNetCore.Authorization;
using System.Net.Mime;

namespace App.Services.Gateway.Controllers;

[Route("api/[controller]")]
[Produces(MediaTypeNames.Application.Json)]
[Consumes(MediaTypeNames.Application.Json)]
public class MatchesController : ApiController
{
    private readonly ITournamentsGrpcService _tournamentsGrpcService;

    public MatchesController(ITournamentsGrpcService tournamentsGrpcService)
    {
        _tournamentsGrpcService = tournamentsGrpcService;
    }

    /// <summary>
    ///     Get all matches
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [HttpGet]
    [Route("")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(GetMatchesGrpcCommandResult))]
    public Task<IActionResult> GetAllMatches(string? teamId = null, string? tournamentId = null, string? winningTeamId = null)
    {
        return TryAsync(() => _tournamentsGrpcService.GetMatches(CreateCommandMessage<GetMatchesGrpcCommandMessage>(
            message =>
            {
                message.TeamId = teamId;
                message.TournamentId = tournamentId;
                message.WinningTeamId = winningTeamId;
            })));
    }

    /// <summary>
    ///     Get match by id
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [HttpGet]
    [Route("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(GetMatchByIdGrpcCommandResult))]
    [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(IGrpcCommandResult))]
    public Task<IActionResult> GetMatchById(string id)
    {
        return TryAsync(() => _tournamentsGrpcService.GetMatchById(CreateCommandMessage<GetMatchByIdGrpcCommandMessage>(message => message.Id = id)));
    }

    /// <summary>
    ///     Create a match
    /// </summary>
    /// <param name="model"></param>
    /// <returns></returns>
    [HttpPost]
    [Route(""), Authorize]
    [ProducesResponseType(StatusCodes.Status202Accepted, Type = typeof(CreateMatchGrpcCommandResult))]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(IGrpcCommandResult))]
    [ProducesResponseType(StatusCodes.Status401Unauthorized, Type = typeof(IGrpcCommandResult))]
    public Task<IActionResult> CreateMatch([FromBody] CreateMatchModel model)
    {
        return TryAsync(() => _tournamentsGrpcService.CreateMatch(CreateCommandMessage<CreateMatchGrpcCommandMessage>(message =>
        {
            message.Name = model.Name;
            message.TeamsId = model.TeamsId;
            message.TournamentId = model.TournamentId;
        })), true);
    }

    /// <summary>
    ///     Update a match by id
    /// </summary>
    /// <param name="model"></param>
    /// <returns></returns>
    [HttpPut]
    [Route("{id}"), Authorize]
    [ProducesResponseType(StatusCodes.Status202Accepted, Type = typeof(UpdateMatchGrpcCommandResult))]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(IGrpcCommandResult))]
    [ProducesResponseType(StatusCodes.Status401Unauthorized, Type = typeof(IGrpcCommandResult))]
    [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(IGrpcCommandResult))]
    public Task<IActionResult> UpdateMatch(string id, [FromBody] UpdateMatchModel model)
    {
        return TryAsync(() => _tournamentsGrpcService.UpdateMatch(CreateCommandMessage<UpdateMatchGrpcCommandMessage>(message =>
        {
            message.Id = id;
            message.Name = model.Name;
            message.WinningTeamId = model.WinningTeamId;
        })), true);
    }

    /// <summary>
    ///     Delete a match by id
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [HttpDelete]
    [Route("{id}"), Authorize]
    [ProducesResponseType(StatusCodes.Status202Accepted, Type = typeof(DeleteMatchByIdGrpcCommandResult))]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(IGrpcCommandResult))]
    [ProducesResponseType(StatusCodes.Status401Unauthorized, Type = typeof(IGrpcCommandResult))]
    [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(IGrpcCommandResult))]
    public Task<IActionResult> DeleteMatchById(string id)
    {
        return TryAsync(() => _tournamentsGrpcService.DeleteMatchById(CreateCommandMessage<DeleteMatchByIdGrpcCommandMessage>(message => message.Id = id)), true);
    }



    /// <summary>
    ///     Get tournament for match by id
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [HttpGet]
    [Route("{id}/tournament")]
    [ProducesResponseType(StatusCodes.Status202Accepted, Type = typeof(GetTournamentByMatchIdGrpcCommandResult))]
    [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(IGrpcCommandResult))]
    public Task<IActionResult> GetTournamentByMatchId(string id)
    {
        return TryAsync(() => _tournamentsGrpcService.GetTournamentByMatchId(CreateCommandMessage<GetTournamentByMatchIdGrpcCommandMessage>(message => message.MatchId = id)));
    }
}