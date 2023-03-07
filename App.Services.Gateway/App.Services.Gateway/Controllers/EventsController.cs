using App.Common.Grpc;
using App.Services.Events.Infrastructure.Grpc;
using App.Services.Events.Infrastructure.Grpc.CommandMessages;
using App.Services.Events.Infrastructure.Grpc.CommandResults;
using App.Services.Gateway.Common;
using App.Services.Gateway.Infrastructure;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net.Mime;

namespace App.Services.Gateway.Controllers;

[Route("api/[controller]")]
[Produces(MediaTypeNames.Application.Json)]
[Consumes(MediaTypeNames.Application.Json)]
public class EventsController : ApiController
{
    private readonly IEventsGrpcService _eventsGrpcService;

    public EventsController(IEventsGrpcService eventsGrpcService)
    {
        this._eventsGrpcService = eventsGrpcService;
    }

    /// <summary>
    ///     Get all events
    /// </summary>
    /// <param name="searchText">if specified will search events with this</param>
    /// <param name="departmentId">if specified will return events in this department</param>
    /// <returns></returns>
    [HttpGet]
    [Route("")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(GetEventsGrpcCommandResult))]
    public Task<IActionResult> GetAllEvents(string? searchText = null, string? departmentId = null)
    {
        return this.TryAsync(() => this._eventsGrpcService.GetEvents(CreateCommandMessage<GetEventsGrpcCommandMessage>(
            message =>
            {
                message.SearchText = searchText;
                message.DepartmentId = departmentId;
            })));
    }

    /// <summary>
    ///     Get event by id
    /// </summary>
    /// <param name="id">id of the event</param>
    /// <returns></returns>
    [HttpGet]
    [Route("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(GetEventByIdGrpcCommandResult))]
    [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(IGrpcCommandResult))]
    public Task<IActionResult> GetEventById(string id)
    {
        return this.TryAsync(() =>
            this._eventsGrpcService.GetEventById(CreateCommandMessage<GetEventByIdGrpcCommandMessage>(message => message.Id = id)));
    }

    /// <summary>
    ///     Create an event
    /// </summary>
    /// <param name="model">data required</param>
    /// <returns></returns>
    [HttpPost]
    [Route("")]
    [ProducesResponseType(StatusCodes.Status202Accepted, Type = typeof(CreateEventGrpcCommandResult))]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(IGrpcCommandResult))]
    public Task<IActionResult> CreateEvent([FromBody] CreateEventModel model)
    {
        return this.TryAsync(() => this._eventsGrpcService.CreateEvent(CreateCommandMessage<CreateEventGrpcCommandMessage>(message =>
        {
            message.EventName = model.EventName;
            message.Location = model.Location;
            message.StartDate = model.StartDate;
            message.EndDate = model.EndDate;
        })));
    }

    /// <summary>
    ///     Update an event by id
    /// </summary>
    /// <param name="id"></param>
    /// <param name="model">data required</param>
    /// <returns></returns>
    [HttpPut]
    [Route("{id}")]
    [ProducesResponseType(StatusCodes.Status202Accepted, Type = typeof(UpdateEventGrpcCommandResult))]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(IGrpcCommandResult))]
    [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(IGrpcCommandResult))]
    public Task<IActionResult> UpdateEvent(string id, [FromBody] UpdateEventModel model)
    {
        return this.TryAsync(() => this._eventsGrpcService.UpdateEvent(CreateCommandMessage<UpdateEventGrpcCommandMessage>(message =>
            {
                message.Id = id;
                message.EventName = model.Name;
                message.Location = model.Location;
                message.StartDate = model.StartDate;
                message.EndDate = model.EndDate;
            })));
    }

    /// <summary>
    ///     Delete an event by id
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [HttpDelete]
    [Route("{id}")]
    [ProducesResponseType(StatusCodes.Status202Accepted, Type = typeof(DeleteEventGrpcCommandResult))]
    [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(IGrpcCommandResult))]
    public Task<IActionResult> DeleteEvent(string id)
    {
        return this.TryAsync(() => this._eventsGrpcService.DeleteEvent(CreateCommandMessage<DeleteEventGrpcCommandMessage>(message => message.Id = id)));
    }
}