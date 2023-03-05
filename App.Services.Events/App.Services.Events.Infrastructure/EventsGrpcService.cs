using App.Common.Grpc;
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

namespace App.Services.Events.Infrastructure;

public class EventsGrpcService : BaseGrpcService, IEventsGrpcService
{
    private readonly IEntityDataService _entityDataService;

    private readonly IMapper _mapper;

    private readonly IPublishEndpoint _publishEndpoint;

    public EventsGrpcService(IEntityDataService entityDataService, IMapper mapper, IPublishEndpoint publishEndpoint)
    {
        this._entityDataService = entityDataService;
        this._mapper = mapper;
        this._publishEndpoint = publishEndpoint;
    }

    public ValueTask<GetEventByIdGrpcCommandResult> GetEventById(GetEventByIdGrpcCommandMessage message)
    {
        return this.TryAsync(async () =>
        {
            var @event = await this._entityDataService.GetEntity<EventEntity>(message.Id);

            return new GetEventByIdGrpcCommandResult
            {
                Metadata = new GrpcCommandResultMetadata
                {
                    Success = true
                },
                Event = this._mapper.Map<EventDto>(@event)
            };
        });
    }

    public ValueTask<CreateEventGrpcCommandResult> CreateEvent(CreateEventGrpcCommandMessage message)
    {
        return this.TryAsync(async () =>
        {
            var @event = new CreateEventCommandMessage
            {
                EventName = message.EventName,
                Location = message.Location,
                StartDate = message.StartDate,
                EndDate = message.EndDate
            };

            await this._publishEndpoint.Publish(@event);
            ;

            return new CreateEventGrpcCommandResult
            {
                Metadata = new GrpcCommandResultMetadata { Success = true }
            };
        });
    }

    public ValueTask<UpdateEventGrpcCommandResult> UpdateEvent(UpdateEventGrpcCommandMessage message)
    {
        return this.TryAsync(async () =>
        {
            var updateMessage = new UpdateEventCommandMessage
            {
                Id = message.Id,
                Location = message.Location,
                StartDate = message.StartDate,
                EndDate = message.EndDate,
                EventName = message.EventName
            };

            await this._publishEndpoint.Publish(updateMessage);

            return new UpdateEventGrpcCommandResult
            {
                Metadata = new GrpcCommandResultMetadata { Success = true }
            };
        });
    }

    public ValueTask<DeleteEventGrpcCommandResult> DeleteEvent(DeleteEventGrpcCommandMessage message)
    {
        return this.TryAsync(async () =>
        {
            var deleteMessage = new DeleteEventCommandMessage
            {
                Id = message.Id
            };

            await this._publishEndpoint.Publish(deleteMessage);

            return new DeleteEventGrpcCommandResult
            {
                Metadata = new GrpcCommandResultMetadata { Success = true }
            };
        });
    }

    public ValueTask<GetEventsGrpcCommandResult> GetEvents(GetEventsGrpcCommandMessage mesage)
    {
        return this.TryAsync(async () =>
        {
            var events = await this._entityDataService.ListEntities<EventEntity>();

            return new GetEventsGrpcCommandResult
            {
                Metadata = new GrpcCommandResultMetadata { Success = true },
                Events = this._mapper.Map<EventDto[]>(events)
            };
        });
    }
}