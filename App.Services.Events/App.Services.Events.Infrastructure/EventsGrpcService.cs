using App.Data.Services;
using App.Infrastructure.Grpc;
using App.Services.Events.Common.Dtos;
using App.Services.Events.Data.Entities;
using App.Services.Events.Infrastructure.Commands;
using App.Services.Events.Infrastructure.Grpc;
using App.Services.Events.Infrastructure.Grpc.CommandMessages;
using App.Services.Events.Infrastructure.Grpc.CommandResults;
using AutoMapper;
using MassTransit;
using ProtoBuf.Grpc.Configuration;

namespace App.Services.Events.Infrastructure
{
    public class EventsGrpcService : BaseGrpcService, IEventsGrpcService
    {
        private readonly IEntityDataService _entityDataService;

        private readonly IMapper _mapper;

        private IPublishEndpoint _publishEndpoint;
        public EventsGrpcService(IEntityDataService entityDataService, IMapper mapper, IPublishEndpoint publishEndpoint)
        {
            _entityDataService = entityDataService;
            _mapper = mapper;
            _publishEndpoint = publishEndpoint;
        }

        public ValueTask<GetEventByIdGrpcCommandResult> GetEventById(GetEventByIdGrpcCommandMessage message)
        {
            return TryAsync(async () =>
            {
                var @event = await _entityDataService.GetEntity<EventEntity>(message.Id);
                return new GetEventByIdGrpcCommandResult
                {
                    Metadata = new GrpcCommandResultMetadata
                    {
                        Success = true
                    },
                    Event = _mapper.Map<EventDto>(@event)
                };
            });
        }

        public ValueTask<CreateEventGrpcCommandResult> CreateEvent(CreateEventGrpcCommandMessage message)
        {
            return TryAsync(async () =>
            {
                var @event = new CreateEventCommandMessage
                {
                    EventName = message.EventName,
                    Location = message.Location,
                    StartDate = message.StartDate,
                    EndDate = message.EndDate,
                };

                await _publishEndpoint.Publish(@event);;

                return new CreateEventGrpcCommandResult
                {
                    Metadata = new GrpcCommandResultMetadata { Success = true },
                };
            });
        }

    }
}