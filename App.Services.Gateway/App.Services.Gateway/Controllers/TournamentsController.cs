using System.Net.Mime;
using App.Services.Gateway.Common;
using App.Services.Gateway.Infrastructure;
using App.Services.Tournaments.Common.Dtos;
using App.Services.Tournaments.Infrastructure.Grpc;
using App.Services.Tournaments.Infrastructure.Grpc.CommandMessages;
using Microsoft.AspNetCore.Mvc;

namespace App.Services.Gateway.Controllers;

[Route("api/[controller]")]
[Produces(MediaTypeNames.Application.Json)]
[Consumes(MediaTypeNames.Application.Json)]
public class TournamentsController : ApiController
{
    private readonly ITournamentsGrpcService _tournamentsGrpcService;

    public TournamentsController(ITournamentsGrpcService tournamentsGrpcService)
    {
        _tournamentsGrpcService = tournamentsGrpcService;
    }

    #region Turnaments

    /// <summary>
    ///     Gets all tournaments
    /// </summary>
    /// <returns></returns>
    [HttpGet]
    [Route("")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public Task<IActionResult> GetAllTournaments()
    {
        return TryAsync(() => _tournamentsGrpcService.GetAllTournaments(new GetAllTournamentsGrpcCommandMessage()));
    }

    /// <summary>
    ///     Get all tournaments that is part of an event based on id of the event
    /// </summary>
    /// <param name="eventid"></param>
    /// <returns></returns>
    [HttpGet]
    [Route("{eventid}/event")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public Task<IActionResult> GetTournamentsByEventId(string eventid)
    {
        return TryAsync(() =>
            _tournamentsGrpcService.GetTournamentsByEventId(new GetTournamentsByEventIdGrpcCommandMessage
                { EventId = eventid }));
    }

    /// <summary>
    ///     Gets all tournaments that is playing a game based on id
    /// </summary>
    /// <param name="gameid"></param>
    /// <returns></returns>
    [HttpGet]
    [Route("{gameid}/game")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public Task<IActionResult> GetTournamentsByGameId(string gameid)
    {
        return TryAsync(() =>
            _tournamentsGrpcService.GetTournamentsByGameId(
                new GetTournamentsByGameIdGrpcCommandMessage { GameId = gameid }));
    }

    /// <summary>
    ///     Get a tournament where a match is played based on id of the match
    /// </summary>
    /// <param name="matchid"></param>
    /// <returns></returns>
    [HttpGet]
    [Route("{matchid}/match")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public Task<IActionResult> GetTournamentByMatchId(string matchid)
    {
        return TryAsync(() =>
            _tournamentsGrpcService.GetTournamentByMatchId(new GetTournamentByMatchIdGrpcCommandMessage
                { MatchId = matchid }));
    }

    /// <summary>
    ///     Get tournament by id
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [HttpGet]
    [Route("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public Task<IActionResult> GetTournamentById(string id)
    {
        return TryAsync(() =>
            _tournamentsGrpcService.GetTournamentById(new GetTournamentByIdGrpcCommandMessage { Id = id }));
    }

    /// <summary>
    ///     Create a tournament
    /// </summary>
    /// <param name="model"></param>
    /// <returns></returns>
    [HttpPost]
    [Route("")]
    [ProducesResponseType(StatusCodes.Status202Accepted)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public Task<IActionResult> CreateTournament([FromBody] CreateTournamentModel model)
    {
        return TryAsync(() =>
        {
            var command = new CreateTournamentGrpcCommandMessage
            {
                Name = model.Name,
                GameId = model.GameId,
                EventId = model.EventId
            };

            return _tournamentsGrpcService.CreateTournament(command);
        });
    }

    /// <summary>
    ///     Update a tournament
    /// </summary>
    /// <param name="model"></param>
    /// <returns></returns>
    [HttpPut]
    [Route("{id}")]
    [ProducesResponseType(StatusCodes.Status202Accepted)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public Task<IActionResult> UpdateTournament(string id, [FromBody] UpdateTournamentModel model)
    {
        return TryAsync(() =>
        {
            var command = new UpdateTournamentGrpcCommandMessage
            {
                TournamentDto = new TournamentDto
                {
                    Id = id,
                    Name = model.Name,
                    GameId = model.GameId
                }
            };

            return _tournamentsGrpcService.UpdateTournament(command);
        });
    }

    /// <summary>
    ///     Delete a tournament
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [HttpDelete]
    [Route("{id}")]
    [ProducesResponseType(StatusCodes.Status202Accepted)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public Task<IActionResult> DeleteTournamentById(string id)
    {
        return TryAsync(() =>
            _tournamentsGrpcService.DeleteTournamentById(new DeleteTournamentByIdGrpcCommandMessage { Id = id }));
    }

    #endregion

    #region Matches

    /// <summary>
    ///     Gets all matches in a tournament based on id of the tournament
    /// </summary>
    /// <param name="turnamentid"></param>
    /// <returns></returns>
    [HttpGet]
    [Route("matches/{turnamentid}/turnament")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public Task<IActionResult> GetMatchesByTournamentId(string turnamentid)
    {
        return TryAsync(() =>
            _tournamentsGrpcService.GetMatchesByTournamentId(new GetMatchesByTurnamentIdGrpcCommandMessage
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
            _tournamentsGrpcService.GetMatchesByTeamId(new GetMatchesByTeamIdGrpcCommandMessage { TeamId = teamid }));
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
        return TryAsync(() => _tournamentsGrpcService.GetMatchById(new GetMatchByIdGrpcCommandMessage { Id = id }));
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

            return _tournamentsGrpcService.CreateMatch(command);
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

            return _tournamentsGrpcService.UpdateMatch(command);
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
            _tournamentsGrpcService.DeleteMatchById(new DeleteMatchByIdGrpcCommandMessage { Id = id }));
    }

    #endregion
}