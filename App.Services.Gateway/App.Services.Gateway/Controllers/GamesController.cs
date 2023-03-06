using System.Net.Mime;
using System.Xml.Linq;
using App.Common.Grpc;
using App.Services.Games.Common.Dtos;
using App.Services.Games.Infrastructure.Grpc;
using App.Services.Games.Infrastructure.Grpc.CommandMessages;
using App.Services.Games.Infrastructure.Grpc.CommandResults;
using App.Services.Gateway.Common;
using App.Services.Gateway.Infrastructure;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace App.Services.Gateway.Controllers;

[Route("api/[controller]")]
[Produces(MediaTypeNames.Application.Json)]
[Consumes(MediaTypeNames.Application.Json)]
public class GamesController : ApiController
{
    private readonly IGamesGrpcService _gamesGrpcService;

    public GamesController(IGamesGrpcService gamesGrpcService)
    {
        this._gamesGrpcService = gamesGrpcService;
    }

    /// <summary>
    ///     Get all games
    /// </summary>
    /// <param name="name">if specified will return games search by name</param>
    /// <param name="genre">if specified will return game search by genre</param>
    /// <returns></returns>
    [HttpGet]
    [Route("")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(GetAllGamesGrpcCommandResult))]
    public Task<IActionResult> GetAllGames(string? name = null, string? genre = null)
    {
        if (!string.IsNullOrEmpty(name))
            return this.TryAsync(() =>
                this._gamesGrpcService.GetGamesByName(CreateCommandMessage<GetGamesByNameGrpcCommandMessage>(message => message.Name = name)));

        if (!string.IsNullOrEmpty(genre))
            return this.TryAsync(() =>
                this._gamesGrpcService.GetGamesByGenre(CreateCommandMessage<GetGamesByGenreGrpcCommandMessage>(message => message.Genre = genre)));

        return this.TryAsync(() => this._gamesGrpcService.GetAllGames(CreateCommandMessage<GetAllGamesGrpcCommandMessage>()));
    }

    /// <summary>
    ///     Get a game by id
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [HttpGet]
    [Route("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(GetGameByIdGrpcCommandMessage))]
    [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(IGrpcCommandResult))]
    public Task<IActionResult> GetGameById(string id)
    {
        return this.TryAsync(() => this._gamesGrpcService.GetGameById(CreateCommandMessage<GetGameByIdGrpcCommandMessage>(message => message.Id = id)));
    }

    /// <summary>
    ///     Create a game
    /// </summary>
    /// <param name="model"></param>
    /// <returns></returns>
    [HttpPost]
    [Route(""), Authorize]
    [ProducesResponseType(StatusCodes.Status202Accepted, Type = typeof(CreateGameGrpcCommandMessage))]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(IGrpcCommandResult))]
    public Task<IActionResult> CreateGame([FromBody] CreateGameModel model)
    {
        return this.TryAsync(() => this._gamesGrpcService.CreateGame(CreateCommandMessage<CreateGameGrpcCommandMessage>(message =>
            {
                message.Name = model.Name;
                message.Description = model.Description;
                message.ProfilePicture = model.ProfilePicture;
                message.CoverPicture = model.CoverPicture;
                message.Genre = model.Genre;
            })), true);
    }

    /// <summary>
    ///     Update a game
    /// </summary>
    /// <param name="id"></param>
    /// <param name="model"></param>
    /// <returns></returns>
    [HttpPut]
    [Route("{id}"), Authorize]
    [ProducesResponseType(StatusCodes.Status202Accepted, Type = typeof(UpdateGameGrpcCommandMessage))]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(IGrpcCommandResult))]
    [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(IGrpcCommandResult))]
    public Task<IActionResult> UpdateGame(string id, [FromBody] UpdateGameModel model)
    {
        return this.TryAsync(() => this._gamesGrpcService.updateGame(CreateCommandMessage<UpdateGameGrpcCommandMessage>(message =>
            {
                message.Id = id;
                message.Name = model.Name;
                message.Discription = model.Description;
                message.ProfilePicture = model.ProfilePicture;
                message.CoverPicture = model.CoverPicture;
                message.Genre = model.Genre;
            })), true);
    }

    /// <summary>
    ///     Delete a game
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [HttpDelete]
    [Route("{id}"), Authorize]
    [ProducesResponseType(StatusCodes.Status202Accepted, Type = typeof(DeleteGameByIdGrpcCommandMessage))]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(IGrpcCommandResult))]
    [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(IGrpcCommandResult))]
    public Task<IActionResult> DeleteGameById(string id)
    {
        return this.TryAsync(
            () => this._gamesGrpcService.DeleteGameById(CreateCommandMessage<DeleteGameByIdGrpcCommandMessage>(message => message.Id = id)), true);
    }
}