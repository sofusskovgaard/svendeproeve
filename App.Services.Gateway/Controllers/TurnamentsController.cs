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

        [HttpGet]
        [Route("getallturnaments")]
        public Task<IActionResult> GetAllTurnaments()
        {
            return TryAsync(() => this._turnamentsGrpcService.GetAllTurnaments(new GetAllTurnamentsCommandMessage()));
        }

        [HttpGet]
        [Route("eventid")]
        public Task<IActionResult> GetTurnamentsByEventId(string eventId)
        {
            return TryAsync(() => this._turnamentsGrpcService.GetTurnamentsByEventId(new GetTurnamentsByEventIdCommandMessage() { EventId = eventId }));
        }

        [HttpGet]
        [Route("gameid")]
        public Task<IActionResult> GetTurnamentsByGameId(string gameId)
        {
            return TryAsync(() => this._turnamentsGrpcService.GetTurnamentsByGameId(new GetTurnamentsByGameIdCommandMessage() { GameId = gameId }));
        }

        [HttpGet]
        [Route("matchid")]
        public Task<IActionResult> GetTurnamentByMatchId(string matchId)
        {
            return TryAsync(() => this._turnamentsGrpcService.GetTurnamentByMatchId(new GetTurnamentByMatchIdCommandMessage() { MatchId = matchId }));
        }

        [HttpGet]
        [Route("{id}")]
        public Task<IActionResult> GetTurnamentById(string id)
        {
            return TryAsync(() => this._turnamentsGrpcService.GetTuenamentById(new GetTurnamentByIdCommandMessage() { Id = id }));
        }

        [HttpPost]
        [Route("createturnament")]
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

        [HttpPost]
        [Route("updateturnament")]
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
    }

    public record CreateTurnamentModel(string Name, string GameId, string[] MatchesId, string EventId);
    public record UpdateTurnamentModel(string Id, string Name, string GameId, string[] MatchesId, string EventId);
}
