using App.Services.Gateway.Infrastructure;
using App.Services.Turnaments.Common.Dtos;
using App.Services.Turnaments.Infrastructure.Grpc;
using App.Services.Turnaments.Infrastructure.Grpc.CommandMessages;
using Microsoft.AspNetCore.Mvc;

namespace App.Services.Gateway.Controllers
{
    [Route("api/[controller]")]
    public class TurnamentsController : ApiController
    {
        private readonly ITurnamentsGrpcService _turnamentsGrpcService;
        public TurnamentsController(ITurnamentsGrpcService turnamentsGrpcService)
        {
            _turnamentsGrpcService = turnamentsGrpcService;
        }

        #region Turnaments

        [HttpGet]
        [Route("")]
        public Task<IActionResult> GetAllTurnaments()
        {
            return TryAsync(() => this._turnamentsGrpcService.GetAllTurnaments(new GetAllTurnamentsCommandMessage()));
        }

        [HttpGet]
        [Route("{eventid}/event")]
        public Task<IActionResult> GetTurnamentsByEventId(string eventid)
        {
            return TryAsync(() => this._turnamentsGrpcService.GetTurnamentsByEventId(new GetTurnamentsByEventIdCommandMessage() { EventId = eventid }));
        }

        [HttpGet]
        [Route("{gameid}/game")]
        public Task<IActionResult> GetTurnamentsByGameId(string gameid)
        {
            return TryAsync(() => this._turnamentsGrpcService.GetTurnamentsByGameId(new GetTurnamentsByGameIdCommandMessage() { GameId = gameid }));
        }

        [HttpGet]
        [Route("{matchid}/match")]
        public Task<IActionResult> GetTurnamentByMatchId(string matchid)
        {
            return TryAsync(() => this._turnamentsGrpcService.GetTurnamentByMatchId(new GetTurnamentByMatchIdCommandMessage() { MatchId = matchid }));
        }

        [HttpGet]
        [Route("{id}")]
        public Task<IActionResult> GetTurnamentById(string id)
        {
            return TryAsync(() => this._turnamentsGrpcService.GetTuenamentById(new GetTurnamentByIdCommandMessage() { Id = id }));
        }

        [HttpPost]
        [Route("")]
        public Task<IActionResult> CreateTurnament([FromBody] CreateTurnamentModel model)
        {
            return TryAsync(() =>
            {
                var command = new CreateTurnamentCommandMessage
                {
                    Name = model.Name,
                    GameId = model.GameId,
                    MatchesId = model.MatchesId,
                    EventId = model.EventId
                };

                return this._turnamentsGrpcService.CreateTurnament(command);
            });
        }

        [HttpPut]
        [Route("")]
        public Task<IActionResult> UpdateTurnament([FromBody] UpdateTurnamentModel model)
        {
            return TryAsync(() =>
            {
                var command = new UpdateTurnamentCommandMessage
                {
                    TurnamentDto = new TurnamentDto
                    {
                        Id = model.Id,
                        Name = model.Name,
                        GameId = model.GameId,
                        MatchesId = model.MatchesId,
                        EventId = model.EventId
                    }
                };

                return this._turnamentsGrpcService.UpdateTurnament(command);
            });
        }

        [HttpDelete]
        [Route("{id}")]
        public Task<IActionResult> DeleteTurnamentById(string id)
        {
            return TryAsync(() => this._turnamentsGrpcService.DeleteTurnamentById(new DeleteTurnamentByIdCommandMessage() { Id = id }));
        }

        #endregion
        #region Matches

        [HttpGet]
        [Route("matches/{turnamentid}/turnament")]
        public Task<IActionResult> GetMatchesByTurnamentId(string turnamentid)
        {
            return TryAsync(() => this._turnamentsGrpcService.GetMatchesByTurnamentId(new GetMatchesByTurnamentIdCommandMessage() { TurnamentId = turnamentid }));
        }

        [HttpGet]
        [Route("matches/{teamid}/team")]
        public Task<IActionResult> GetMatchesByTeamId(string teamid)
        {
            return TryAsync(() => this._turnamentsGrpcService.GetMatchesByTeamId(new GetMatchesByTeamIdCommandMessage() { TeamId = teamid }));
        }

        [HttpGet]
        [Route("matches/{id}")]
        public Task<IActionResult> GetMatchById(string id)
        {
            return TryAsync(() => this._turnamentsGrpcService.GetMatchById(new GetMatchByIdCommandMessage() { Id = id }));
        }

        [HttpPost]
        [Route("matches")]
        public Task<IActionResult> CreateMatch([FromBody] CreateMatchModel model)
        {
            return TryAsync(() =>
            {
                var command = new CreateMatchCommandMessage
                {
                    Name = model.Name,
                    TeamsId = model.TeamsId,
                    TurnamentId = model.TurnamentId
                };

                return this._turnamentsGrpcService.CreateMatch(command);
            });
        }

        [HttpPut]
        [Route("matches")]
        public Task<IActionResult> UpdateMatch([FromBody] UpdateMatchModel model)
        {
            return TryAsync(() =>
            {
                var command = new UpdateMatchCommandMessage
                {
                    MatchDto = new MatchDto
                    {
                        Id = model.Id,
                        Name = model.Name,
                        TeamsId = model.TeamsId,
                        TurnamentId = model.TurnamentId,
                        WinningTeamId = model.WinningTeamId
                    }
                };

                return this._turnamentsGrpcService.UpdateMatch(command);
            });
        }

        [HttpDelete]
        [Route("matches/{id}")]
        public Task<IActionResult> DeleteMatchById(string id)
        {
            return TryAsync(() => this._turnamentsGrpcService.DeleteMatchById(new DeleteMatchByIdCommandMessage() { Id = id }));
        }

        #endregion
    }

    public record CreateTurnamentModel(string Name, string GameId, string[] MatchesId, string EventId);
    public record UpdateTurnamentModel(string Id, string Name, string GameId, string[] MatchesId, string EventId);
    public record CreateMatchModel(string Name, string[] TeamsId, string TurnamentId);
    public record UpdateMatchModel(string Id, string Name, string[] TeamsId, string TurnamentId, string WinningTeamId);
}
