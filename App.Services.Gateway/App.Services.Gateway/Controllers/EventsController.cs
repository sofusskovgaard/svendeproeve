using App.Services.Events.Infrastructure.Grpc;
using App.Services.Events.Infrastructure.Grpc.CommandMessages;
using App.Services.Events.Infrastructure.Grpc.CommandResults;
using App.Services.Gateway.Common;
using App.Services.Gateway.Infrastructure;
using Microsoft.AspNetCore.Mvc;

namespace App.Services.Gateway.Controllers;

[Route("api/[controller]")]
public class EventsController : ApiController
{
    private readonly IEventsGrpcService _eventsGrpcService;

    public EventsController(IEventsGrpcService eventsGrpcService)
    {
        this._eventsGrpcService = eventsGrpcService;
    }

    /// <summary>
    ///     Get event by id
    /// </summary>
    /// <param name="id">id of the event</param>
    /// <returns></returns>
    [HttpGet]
    [Route("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(GetEventByIdGrpcCommandResult))]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public Task<IActionResult> GetEventById(string id)
    {
        return this.TryAsync(() =>
            this._eventsGrpcService.GetEventById(new GetEventByIdGrpcCommandMessage { Id = id }));
    }

    [HttpPost]
    [Route("")]
    [ProducesResponseType(StatusCodes.Status202Accepted)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public Task<IActionResult> CreateEvent([FromBody] CreateEventModel model)
    {
        return this.TryAsync(() =>
        {
            var command = new CreateEventGrpcCommandMessage
            {
                EventName = model.EventName,
                Location = model.Location,
                StartDate = model.StartDate,
                EndDate = model.EndDate
            };

            return this._eventsGrpcService.CreateEvent(command);
        });
    }

    [HttpPut]
    [Route("{id}")]
    public Task<IActionResult> UpdateEvent(string id, [FromBody] UpdateEventModel model)
    {
        return this.TryAsync(() =>
        {
            return this._eventsGrpcService.UpdateEvent(new UpdateEventGrpcCommandMessage
            {
                Id = id,
                EventName = model.Name,
                Location = model.Location,
                StartDate = model.StartDate,
                EndDate = model.EndDate
            });
        });
    }

    [HttpDelete]
    [Route("{id}")]
    public Task<IActionResult> DeleteEvent(string id)
    {
        return this.TryAsync(() =>
        {
            return this._eventsGrpcService.DeleteEvent(new DeleteEventGrpcCommandMessage { Id = id });
        });
    }
}