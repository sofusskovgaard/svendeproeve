﻿using System.Net.Mime;
using App.Services.Games.Common.Dtos;
using App.Services.Games.Infrastructure.Grpc;
using App.Services.Games.Infrastructure.Grpc.CommandMessages;
using App.Services.Gateway.Common;
using App.Services.Gateway.Infrastructure;
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
    /// <returns></returns>
    [HttpGet]
    [Route("")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public Task<IActionResult> GetAllGames()
    {
        return this.TryAsync(() => this._gamesGrpcService.GetAllGames(new GetAllGamesGrpcCommandMessage()));
    }

    /// <summary>
    ///     Get all games with the same name
    /// </summary>
    /// <param name="name"></param>
    /// <returns></returns>
    [HttpGet]
    [Route("{name}/name")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public Task<IActionResult> GetGamesByName(string name)
    {
        return this.TryAsync(() =>
            this._gamesGrpcService.GetGamesByName(new GetGamesByNameGrpcCommandMessage { Name = name }));
    }

    /// <summary>
    ///     Get all games that share the given genre
    /// </summary>
    /// <param name="genre"></param>
    /// <returns></returns>
    [HttpGet]
    [Route("{genre}/genre")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public Task<IActionResult> GetGamesByGenre(string genre)
    {
        return this.TryAsync(() =>
            this._gamesGrpcService.GetGamesByGenre(new GetGamesByGenreGrpcCommandMessage { Genre = genre }));
    }

    /// <summary>
    ///     Get a game by id
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [HttpGet]
    [Route("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public Task<IActionResult> GetGameById(string id)
    {
        return this.TryAsync(() => this._gamesGrpcService.GetGameById(new GetGameByIdGrpcCommandMessage { Id = id }));
    }

    /// <summary>
    ///     Create a game
    /// </summary>
    /// <param name="model"></param>
    /// <returns></returns>
    [HttpPost]
    [Route("")]
    [ProducesResponseType(StatusCodes.Status202Accepted)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public Task<IActionResult> CreateGame([FromBody] CreateGameModel model)
    {
        return this.TryAsync(() =>
        {
            var command = new CreateGameGrpcCommandMessage
            {
                Name = model.Name,
                Description = model.Description,
                ProfilePicture = model.ProfilePicture,
                CoverPicture = model.CoverPicture,
                Genre = model.Genre
            };

            return this._gamesGrpcService.CreateGame(command);
        }, true);
    }

    /// <summary>
    ///     Update a game
    /// </summary>
    /// <param name="id"></param>
    /// <param name="model"></param>
    /// <returns></returns>
    [HttpPut]
    [Route("{id}")]
    [ProducesResponseType(StatusCodes.Status202Accepted)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public Task<IActionResult> UpdateGame(string id, [FromBody] UpdateGameModel model)
    {
        return this.TryAsync(() =>
        {
            var command = new UpdateGameGrpcCommandMessage
            {
                GameDto = new GameDto
                {
                    Id = id,
                    Name = model.Name,
                    Discription = model.Description,
                    ProfilePicture = model.ProfilePicture,
                    CoverPicture = model.CoverPicture,
                    Genre = model.Genre
                }
            };

            return this._gamesGrpcService.updateGame(command);
        }, true);
    }

    /// <summary>
    ///     Delete a game
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [HttpDelete]
    [Route("{id}")]
    [ProducesResponseType(StatusCodes.Status202Accepted)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public Task<IActionResult> DeleteGameById(string id)
    {
        return this.TryAsync(
            () => this._gamesGrpcService.DeleteGameById(new DeleteGameByIdGrpcCommandMessage { Id = id }), true);
    }
}