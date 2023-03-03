using App.Services.Gateway.Infrastructure;
using App.Services.Turnaments.Common.Dtos;
using App.Services.Turnaments.Infrastructure.Grpc;
using App.Services.Turnaments.Infrastructure.Grpc.CommandMessages;
using Microsoft.AspNetCore.Mvc;
using System.Net.Mime;

namespace App.Services.Gateway.Controllers
{
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
        /// Gets all turnaments
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public Task<IActionResult> GetAllTurnaments()
        {
            return TryAsync(() => this._turnamentsGrpcService.GetAllTurnaments(new GetAllTurnamentsGrpcCommandMessage()));
        }

        /// <summary>
        /// Get all turnaments that is part of an event based on id of the event
        /// </summary>
        /// <param name="eventid"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("{eventid}/event")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public Task<IActionResult> GetTurnamentsByEventId(string eventid)
        {
            return TryAsync(() => this._turnamentsGrpcService.GetTurnamentsByEventId(new GetTurnamentsByEventIdGrpcCommandMessage() { EventId = eventid }));
        }

        /// <summary>
        /// Gets all turnaments that is playing a game based on id
        /// </summary>
        /// <param name="gameid"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("{gameid}/game")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public Task<IActionResult> GetTurnamentsByGameId(string gameid)
        {
            return TryAsync(() => this._turnamentsGrpcService.GetTurnamentsByGameId(new GetTurnamentsByGameIdGrpcCommandMessage() { GameId = gameid }));
        }

        /// <summary>
        /// Get a turnament where a match is played based on id of the match
        /// </summary>
        /// <param name="matchid"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("{matchid}/match")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public Task<IActionResult> GetTurnamentByMatchId(string matchid)
        {
            return TryAsync(() => this._turnamentsGrpcService.GetTurnamentByMatchId(new GetTurnamentByMatchIdGrpcCommandMessage() { MatchId = matchid }));
        }

        /// <summary>
        /// Get turnament by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public Task<IActionResult> GetTurnamentById(string id)
        {
            return TryAsync(() => this._turnamentsGrpcService.GetTuenamentById(new GetTurnamentByIdGrpcCommandMessage() { Id = id }));
        }

        /// <summary>
        /// Create a turnament
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("")]
        [ProducesResponseType(StatusCodes.Status202Accepted)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public Task<IActionResult> CreateTurnament([FromBody] CreateTurnamentModel model)
        {
            return TryAsync(() =>
            {
                var command = new CreateTurnamentGrpcCommandMessage
                {
                    Name = model.Name,
                    GameId = model.GameId,
                    EventId = model.EventId
                };

                return this._turnamentsGrpcService.CreateTurnament(command);
            });
        }

        /// <summary>
        /// Update a turnament
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPut]
        [Route("")]
        [ProducesResponseType(StatusCodes.Status202Accepted)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public Task<IActionResult> UpdateTurnament([FromBody] UpdateTurnamentModel model)
        {
            return TryAsync(() =>
            {
                var command = new UpdateTurnamentGrpcCommandMessage
                {
                    TurnamentDto = new TurnamentDto
                    {
                        Id = model.Id,
                        Name = model.Name,
                        GameId = model.GameId,
                    }
                };

                return this._turnamentsGrpcService.UpdateTurnament(command);
            });
        }

        /// <summary>
        /// Delete a turnament
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete]
        [Route("{id}")]
        [ProducesResponseType(StatusCodes.Status202Accepted)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public Task<IActionResult> DeleteTurnamentById(string id)
        {
            return TryAsync(() => this._turnamentsGrpcService.DeleteTurnamentById(new DeleteTurnamentByIdGrpcCommandMessage() { Id = id }));
        }

        #endregion
        #region Matches

        /// <summary>
        /// Gets all matches in a turnament based on id of the turnament
        /// </summary>
        /// <param name="turnamentid"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("matches/{turnamentid}/turnament")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public Task<IActionResult> GetMatchesByTurnamentId(string turnamentid)
        {
            return TryAsync(() => this._turnamentsGrpcService.GetMatchesByTurnamentId(new GetMatchesByTurnamentIdGrpcCommandMessage() { TurnamentId = turnamentid }));
        }

        /// <summary>
        /// Gets matches a team is playing in based on id of the team
        /// </summary>
        /// <param name="teamid"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("matches/{teamid}/team")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public Task<IActionResult> GetMatchesByTeamId(string teamid)
        {
            return TryAsync(() => this._turnamentsGrpcService.GetMatchesByTeamId(new GetMatchesByTeamIdGrpcCommandMessage() { TeamId = teamid }));
        }

        /// <summary>
        /// Get a match by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("matches/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public Task<IActionResult> GetMatchById(string id)
        {
            return TryAsync(() => this._turnamentsGrpcService.GetMatchById(new GetMatchByIdGrpcCommandMessage() { Id = id }));
        }

        /// <summary>
        /// Create a match
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

                return this._turnamentsGrpcService.CreateMatch(command);
            });
        }

        /// <summary>
        /// Update a match
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPut]
        [Route("matches")]
        [ProducesResponseType(StatusCodes.Status202Accepted)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public Task<IActionResult> UpdateMatch([FromBody] UpdateMatchModel model)
        {
            return TryAsync(() =>
            {
                var command = new UpdateMatchGrpcCommandMessage
                {
                    MatchDto = new MatchDto
                    {
                        Id = model.Id,
                        Name = model.Name,
                        WinningTeamId = model.WinningTeamId
                    }
                };

                return this._turnamentsGrpcService.UpdateMatch(command);
            });
        }

        /// <summary>
        /// Delete a match
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete]
        [Route("matches/{id}")]
        [ProducesResponseType(StatusCodes.Status202Accepted)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public Task<IActionResult> DeleteMatchById(string id)
        {
            return TryAsync(() => this._turnamentsGrpcService.DeleteMatchById(new DeleteMatchByIdGrpcCommandMessage() { Id = id }));
        }

        #endregion
    }

    /// <summary>
    /// Data required to create a new turnament
    /// </summary>
    /// <param name="Name"></param>
    /// <param name="GameId"></param>
    /// <param name="EventId"></param>
    public record CreateTurnamentModel(string Name, string GameId, string EventId);

    /// <summary>
    /// Data required to update a turnament
    /// </summary>
    /// <param name="Id"></param>
    /// <param name="Name"></param>
    /// <param name="GameId"></param>
    public record UpdateTurnamentModel(string Id, string Name, string GameId);

    /// <summary>
    /// Data required to create a new match
    /// </summary>
    /// <param name="Name"></param>
    /// <param name="TeamsId"></param>
    /// <param name="TurnamentId"></param>
    public record CreateMatchModel(string Name, string[] TeamsId, string TurnamentId);

    /// <summary>
    /// Data required to update a match
    /// </summary>
    /// <param name="Id"></param>
    /// <param name="Name"></param>
    /// <param name="WinningTeamId"></param>
    public record UpdateMatchModel(string Id, string Name, string WinningTeamId);
}
