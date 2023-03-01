﻿using App.Services.Events.Infrastructure.Grpc;
using App.Services.Events.Infrastructure.Grpc.CommandMessages;
using App.Services.Gateway.Infrastructure;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ZstdSharp.Unsafe;

namespace App.Services.Gateway.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EventsController : ApiController
    {
        private readonly IEventsGrpcService _eventsGrpcService;

        public EventsController(IEventsGrpcService eventsGrpcService)
        {
            _eventsGrpcService = eventsGrpcService;
        }

        /// <summary>
        /// Get event by id
        /// </summary>
        /// <param name="id">id of the event</param>
        /// <returns></returns>
        [HttpGet]
        [Route("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public Task<IActionResult> GetEventById(string id)
        {
            return TryAsync(() => _eventsGrpcService.GetEventById(new GetEventByIdGrpcCommandMessage { Id = id }));
        }

        [HttpPost]
        [Route("")]
        [ProducesResponseType(StatusCodes.Status202Accepted)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public Task<IActionResult> CreateEvent([FromBody] CreateEventModel model)
        {
            return TryAsync(() =>
            {
                var command = new CreateEventGrpcCommandMessage
                {
                    EventName = model.EventName,
                    Location = model.Location,
                    StartDate = model.StartDate,
                    EndDate = model.EndDate
                };
                return _eventsGrpcService.CreateEvent(command);
            });
        }
    }

    /// <summary>
    /// Data required to create a new event
    /// </summary>
    /// <param name="EventName">Name of event</param>
    /// <param name="Location">Location of event</param>
    /// <param name="StartDate">Start date of the event</param>
    /// <param name="EndDate">End date of the event</param>
    public record CreateEventModel(string EventName, string Location, DateTime StartDate, DateTime EndDate);
}