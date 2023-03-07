using System.Net.Mime;
using App.Services.Gateway.Common;
using App.Services.Gateway.Infrastructure;
using App.Services.Turnaments.Common.Dtos;
using App.Services.Turnaments.Infrastructure.Grpc;
using App.Services.Turnaments.Infrastructure.Grpc.CommandMessages;
using Microsoft.AspNetCore.Mvc;

namespace App.Services.Gateway.Controllers;

[Route("api/[controller]")]
[Produces(MediaTypeNames.Application.Json)]
[Consumes(MediaTypeNames.Application.Json)]
public class TurnamentsController : ApiController
{
    private readonly ITurnamentsGrpcService _turnamentsGrpcService;

    public TurnamentsController(ITurnamentsGrpcService turnamentsGrpcService)
    {
        _turnamentsGrpcService = turnamentsGrpcService;
    }

    #region Turnaments

    /// <summary>
    ///     Gets all turnaments
    /// </summary>
    /// <returns></returns>
    [HttpGet]
    [Route("")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public Task<IActionResult> GetAllTurnaments()
    {
        return TryAsync(() => _turnamentsGrpcService.GetAllTurnaments(new GetAllTurnamentsGrpcCommandMessage()));
    }

    /// <summary>
    ///     Get all turnaments that is part of an event based on id of the event
    /// </summary>
    /// <param name="eventid"></param>
    /// <returns></returns>
    [HttpGet]
    [Route("{eventid}/event")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public Task<IActionResult> GetTurnamentsByEventId(string eventid)
    {
        return TryAsync(() =>
            _turnamentsGrpcService.GetTurnamentsByEventId(new GetTurnamentsByEventIdGrpcCommandMessage
                { EventId = eventid }));
    }

    /// <summary>
    ///     Gets all turnaments that is playing a game based on id
    /// </summary>
    /// <param name="gameid"></param>
    /// <returns></returns>
    [HttpGet]
    [Route("{gameid}/game")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public Task<IActionResult> GetTurnamentsByGameId(string gameid)
    {
        return TryAsync(() =>
            _turnamentsGrpcService.GetTurnamentsByGameId(
                new GetTurnamentsByGameIdGrpcCommandMessage { GameId = gameid }));
    }

    /// <summary>
    ///     Get a turnament where a match is played based on id of the match
    /// </summary>
    /// <param name="matchid"></param>
    /// <returns></returns>
    [HttpGet]
    [Route("{matchid}/match")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public Task<IActionResult> GetTurnamentByMatchId(string matchid)
    {
        return TryAsync(() =>
            _turnamentsGrpcService.GetTurnamentByMatchId(new GetTurnamentByMatchIdGrpcCommandMessage
                { MatchId = matchid }));
    }

    /// <summary>
    ///     Get turnament by id
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [HttpGet]
    [Route("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public Task<IActionResult> GetTurnamentById(string id)
    {
        return TryAsync(() =>
            _turnamentsGrpcService.GetTuenamentById(new GetTurnamentByIdGrpcCommandMessage { Id = id }));
    }

    /// <summary>
    ///     Create a turnament
    /// </summary>
    /// <param name="model"></param>
    /// <returns></returns>
    [HttpPost]
    [Route("")]
    [ProducesResponseType(StatusCodes.Status202Accepted)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public Task<IActionResult> CreateTurnament([FromBody] CreateTournamentModel model)
    {
        return TryAsync(() =>
        {
            var command = new CreateTurnamentGrpcCommandMessage
            {
                Name = model.Name,
                GameId = model.GameId,
                EventId = model.EventId
            };

            return _turnamentsGrpcService.CreateTurnament(command);
        });
    }

    /// <summary>
    ///     Update a turnament
    /// </summary>
    /// <param name="model"></param>
    /// <returns></returns>
    [HttpPut]
    [Route("{id}")]
    [ProducesResponseType(StatusCodes.Status202Accepted)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public Task<IActionResult> UpdateTurnament(string id, [FromBody] UpdateTournamentModel model)
    {
        return TryAsync(() =>
        {
            var command = new UpdateTurnamentGrpcCommandMessage
            {
                TurnamentDto = new TurnamentDto
                {
                    Id = id,
                    Name = model.Name,
                    GameId = model.GameId
                }
            };

            return _turnamentsGrpcService.UpdateTurnament(command);
        });
    }

    /// <summary>
    ///     Delete a turnament
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [HttpDelete]
    [Route("{id}")]
    [ProducesResponseType(StatusCodes.Status202Accepted)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public Task<IActionResult> DeleteTurnamentById(string id)
    {
        return TryAsync(() =>
            _turnamentsGrpcService.DeleteTurnamentById(new DeleteTurnamentByIdGrpcCommandMessage { Id = id }));
    }

    #endregion

    #region Matches

    /// <summary>
    ///     Gets all matches in a turnament based on id of the turnament
    /// </summary>
    /// <param name="turnamentid"></param>
    /// <returns></returns>
    [HttpGet]
    [Route("matches/{turnamentid}/turnament")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public Task<IActionResult> GetMatchesByTurnamentId(string turnamentid)
    {
        return TryAsync(() =>
            _turnamentsGrpcService.GetMatchesByTurnamentId(new GetMatchesByTurnamentIdGrpcCommandMessage
                { TurnamentId = turnamentid }));
    }

    /// <summary>
    ///     Gets matches a team is playing in based on id of the team
    /// </summary>
    /// <param name="teamid"></param>
    /// <returns></returns>
    [HttpGet]
    [Route("matches/{teamid}/team")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public Task<IActionResult> GetMatchesByTeamId(string teamid)
    {
        return TryAsync(() =>
            _turnamentsGrpcService.GetMatchesByTeamId(new GetMatchesByTeamIdGrpcCommandMessage { TeamId = teamid }));
    }

    /// <summary>
    ///     Get a match by id
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [HttpGet]
    [Route("matches/{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public Task<IActionResult> GetMatchById(string id)
    {
        return TryAsync(() => _turnamentsGrpcService.GetMatchById(new GetMatchByIdGrpcCommandMessage { Id = id }));
    }

    /// <summary>
    ///     Create a match
    /// </summary>
    /// <param name="model"></param>
    /// <returns></returns>
    [HttpPost]
    [Route("matches")]
    [ProducesResponseType(StatusCodes.Status202Accepted)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public Task<IActionResult> CreateMatch([FromBody] CreateMatchModel model)
    {
        return TryAsync(() =>
        {
            var command = new CreateMatchGrpcCommandMessage
            {
                Name = model.Name,
                TeamsId = model.TeamsId,
                TurnamentId = model.TurnamentId
            };

            return _turnamentsGrpcService.CreateMatch(command);
        });
    }

    /// <summary>
    ///     Update a match
    /// </summary>
    /// <param name="model"></param>
    /// <returns></returns>
    [HttpPut]
    [Route("matches/{id}")]
    [ProducesResponseType(StatusCodes.Status202Accepted)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public Task<IActionResult> UpdateMatch(string id, [FromBody] UpdateMatchModel model)
    {
        return TryAsync(() =>
        {
            var command = new UpdateMatchGrpcCommandMessage
            {
                MatchDto = new MatchDto
                {
                    Id = id,
                    Name = model.Name,
                    WinningTeamId = model.WinningTeamId
                }
            };

            return _turnamentsGrpcService.UpdateMatch(command);
        });
    }

    /// <summary>
    ///     Delete a match
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [HttpDelete]
    [Route("matches/{id}")]
    [ProducesResponseType(StatusCodes.Status202Accepted)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public Task<IActionResult> DeleteMatchById(string id)
    {
        return TryAsync(() =>
            _turnamentsGrpcService.DeleteMatchById(new DeleteMatchByIdGrpcCommandMessage { Id = id }));
    }

    #endregion
}