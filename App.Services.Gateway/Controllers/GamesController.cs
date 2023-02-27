using App.Services.Games.Infrastructure.Grpc;
using App.Services.Games.Infrastructure.Grpc.CommandMessages;
using App.Services.Gateway.Infrastructure;
using Microsoft.AspNetCore.Mvc;

namespace App.Services.Gateway.Controllers
{
    [Route("api/[controller]")]
    public class GamesController : ApiController
    {
        private readonly IGamesGrpcService _gamesGrpcService;
        public GamesController(IGamesGrpcService gamesGrpcService)
        {
            _gamesGrpcService = gamesGrpcService;
        }

        [HttpGet]
        [Route("index")]
        public Task<IActionResult> GetAllGames()
        {
            return TryAsync(() => this._gamesGrpcService.GetAllGames(new GetAllGamesCommandMessage()));
        }

        [HttpGet]
        [Route("name")]
        public Task<IActionResult> GetGamesByName(string name)
        {
            return TryAsync(() => this._gamesGrpcService.GetGamesByName(new GetGamesByNameCommandMessage() { Name = name }));
        }

        [HttpGet]
        [Route("Genre")]
        public Task<IActionResult> GetGamesByGenre(string genre)
        {
            return TryAsync(() => this._gamesGrpcService.GetGamesByGenre(new GetGamesByGenreCommandMessage() { Genre = genre }));
        }

        [HttpGet]
        [Route("{id}")]
        public Task<IActionResult> GetGameById(string id)
        {
            return TryAsync(() => this._gamesGrpcService.GetGameById(new GetGameByIdCommandMessage() { Id = id }));
        }

        [HttpPost]
        [Route("create")]
        public Task<IActionResult> CreateGame([FromBody] CreateGameModel model)
        {
            return TryAsync(() =>
            {
                var command = new CreateGameCommandMessage
                {
                    Name = model.Name,
                    Discription = model.Discription,
                    ProfilePicture = model.ProfilePicture,
                    CoverPicture = model.CoverPicture,
                    Genre = model.Genre
                };

                return this._gamesGrpcService.CreateGame(command);
            });
        }

        [HttpDelete]
        [Route("delete")]
        public Task<IActionResult> DeleteGameById(string id)
        {
            return TryAsync(() => this._gamesGrpcService.DeleteGameById(new DeleteGameByIdCommandMessage() { Id = id }));
        }
    }

    public record CreateGameModel(string Name, string Discription, string ProfilePicture, string CoverPicture, string[] Genre);
}
