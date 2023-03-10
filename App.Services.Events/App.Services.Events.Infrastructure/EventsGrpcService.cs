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
using MongoDB.Driver;

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
                Metadata = new GrpcCommandResultMetadata{ Success = true },
                Data = this._mapper.Map<EventDto>(@event)
            };
        });
    }

    public ValueTask<CreateEventGrpcCommandResult> CreateEvent(CreateEventGrpcCommandMessage message)
    {
        return this.TryAsync(async () =>
        {
            await this._publishEndpoint.Publish(new CreateEventCommandMessage
            {
                EventName = message.EventName,
                Location = message.Location,
                StartDate = message.StartDate,
                EndDate = message.EndDate
            });

            return new CreateEventGrpcCommandResult{ Metadata = new GrpcCommandResultMetadata{ Success = true } };
        });
    }

    public ValueTask<UpdateEventGrpcCommandResult> UpdateEvent(UpdateEventGrpcCommandMessage message)
    {
        return this.TryAsync(async () =>
        {
            await this._publishEndpoint.Publish(new UpdateEventCommandMessage
            {
                Id = message.Id,
                Location = message.Location,
                StartDate = message.StartDate,
                EndDate = message.EndDate,
                EventName = message.EventName
            });

            return new UpdateEventGrpcCommandResult{ Metadata = new GrpcCommandResultMetadata{ Success = true } };
        });
    }

    public ValueTask<DeleteEventGrpcCommandResult> DeleteEvent(DeleteEventGrpcCommandMessage message)
    {
        return this.TryAsync(async () =>
        {
            await this._publishEndpoint.Publish(new DeleteEventCommandMessage{ Id = message.Id });

            return new DeleteEventGrpcCommandResult{ Metadata = new GrpcCommandResultMetadata{ Success = true } };
        });
    }

    public ValueTask<GetEventsGrpcCommandResult> GetEvents(GetEventsGrpcCommandMessage message)
    {
        return this.TryAsync(async () =>
        {
            var filters = new List<FilterDefinition<EventEntity>>();

            if (!string.IsNullOrEmpty(message.SearchText))
            {
                filters.Add(new FilterDefinitionBuilder<EventEntity>().Text(message.SearchText));
            }

            if (!string.IsNullOrEmpty(message.DepartmentId))
            {
                filters.Add(new FilterDefinitionBuilder<EventEntity>().Eq(entity => entity.DepartmentId, message.DepartmentId));
            }

            var entities = await _entityDataService.ListEntities<EventEntity>(filter =>
                filters.Any() ? filter.And(filters) : FilterDefinition<EventEntity>.Empty);

            return new GetEventsGrpcCommandResult
            {
                Metadata = new GrpcCommandResultMetadata{ Success = true },
                Data = this._mapper.Map<EventDto[]>(entities)
            };
        });
    }
}