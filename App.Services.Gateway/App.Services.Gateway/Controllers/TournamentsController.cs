using System.Net.Mime;
using System.Xml.Linq;
using App.Common.Grpc;
using App.Services.Gateway.Common;
using App.Services.Gateway.Infrastructure;
using App.Services.Tournaments.Common.Dtos;
using App.Services.Tournaments.Infrastructure.Grpc;
using App.Services.Tournaments.Infrastructure.Grpc.CommandMessages;
using App.Services.Tournaments.Infrastructure.Grpc.CommandResults;
using Microsoft.AspNetCore.Authorization;
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

    /// <summary>
    ///     Get all tournaments
    /// </summary>
    /// <param name="eventId">if specified will return tournaments by this event id</param>
    /// <param name="gameId">if specified will return tournaments by this game id</param>
    /// <returns></returns>
    [HttpGet]
    [Route("")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(GetTournamentsGrpcCommandResult))]
    public Task<IActionResult> GetAllTournaments(string? eventId = null, string? gameId = null)
    {
        return TryAsync(() => _tournamentsGrpcService.GetTournaments(CreateCommandMessage<GetTournamentsGrpcCommandMessage>(
            message =>
            {
                message.EventId = eventId;
                message.GameId = gameId;
            })));
    }

    

    /// <summary>
    ///     Get tournament by id
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [HttpGet]
    [Route("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(GetTournamentByIdGrpcCommandResult))]
    [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(IGrpcCommandResult))]
    public Task<IActionResult> GetTournamentById(string id)
    {
        return TryAsync(() =>
            _tournamentsGrpcService.GetTournamentById(CreateCommandMessage<GetTournamentByIdGrpcCommandMessage>(message => message.Id = id)));
    }

    /// <summary>
    ///     Create a tournament
    /// </summary>
    /// <param name="model"></param>
    /// <returns></returns>
    [HttpPost]
    [Route(""), Authorize]
    [ProducesResponseType(StatusCodes.Status202Accepted, Type = typeof(CreateTournamentGrpcCommandResult))]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(IGrpcCommandResult))]
    [ProducesResponseType(StatusCodes.Status401Unauthorized, Type = typeof(IGrpcCommandResult))]
    [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(IGrpcCommandResult))]
    public Task<IActionResult> CreateTournament([FromBody] CreateTournamentModel model)
    {
        return TryAsync(() => _tournamentsGrpcService.CreateTournament(CreateCommandMessage<CreateTournamentGrpcCommandMessage>(
            message =>
            {
                message.Name = model.Name;
                message.GameId = model.GameId;
                message.EventId = model.EventId;

            })), true);
    }

    /// <summary>
    ///     Update a tournament
    /// </summary>
    /// <param name="model"></param>
    /// <returns></returns>
    [HttpPut]
    [Route("{id}"), Authorize]
    [ProducesResponseType(StatusCodes.Status202Accepted, Type = typeof(UpdateTournamentGrpcCommandResult))]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(IGrpcCommandResult))]
    [ProducesResponseType(StatusCodes.Status401Unauthorized, Type = typeof(IGrpcCommandResult))]
    [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(IGrpcCommandResult))]
    public Task<IActionResult> UpdateTournament(string id, [FromBody] UpdateTournamentModel model)
    {
        return TryAsync(() => _tournamentsGrpcService.UpdateTournament(CreateCommandMessage<UpdateTournamentGrpcCommandMessage>(
            message =>
            {
                message.Id = id;
                message.Name = model.Name;
                message.GameId = model.GameId;
            })), true);
    }

    /// <summary>
    ///     Delete a tournament
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [HttpDelete]
    [Route("{id}"), Authorize]
    [ProducesResponseType(StatusCodes.Status202Accepted, Type = typeof(DeleteTournamentByIdGrpcCommandResult))]
    [ProducesResponseType(StatusCodes.Status401Unauthorized, Type = typeof(IGrpcCommandResult))]
    [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(IGrpcCommandResult))]
    public Task<IActionResult> DeleteTournamentById(string id)
    {
        return TryAsync(() =>
            _tournamentsGrpcService.DeleteTournamentById(CreateCommandMessage<DeleteTournamentByIdGrpcCommandMessage>(message => message.Id = id)), true);
    }
}