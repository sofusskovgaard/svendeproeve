using App.Common.Grpc;
using App.Data.Services;
using App.Infrastructure.Grpc;
using App.Services.Tickets.Common.Dtos;
using App.Services.Tickets.Data.Entities;
using App.Services.Tickets.Infrastructure.Commands;
using App.Services.Tickets.Infrastructure.Events;
using App.Services.Tickets.Infrastructure.Grpc;
using App.Services.Tickets.Infrastructure.Grpc.CommandMessages;
using App.Services.Tickets.Infrastructure.Grpc.CommandResults;
using AutoMapper;
using MassTransit;
using MassTransit.Testing;
using MongoDB.Driver;

namespace App.Services.Tickets.Infrastructure;

public class TicketGrpcService : BaseGrpcService, ITicketGrpcService
{
    private readonly IEntityDataService _entityDataService;

    private readonly IMapper _mapper;

    private readonly IPublishEndpoint _publishEndpoint;

    public TicketGrpcService(IEntityDataService entityDataService, IMapper mapper, IPublishEndpoint publishEndpoint)
    {
        _entityDataService = entityDataService;
        _mapper = mapper;
        _publishEndpoint = publishEndpoint;
    }

    public ValueTask<GetTicketsGrpcCommandResult> GetTickets(GetTicketsGrpcCommandMessage message)
    {
        return TryAsync(async () =>
        {

            var filters = new List<FilterDefinition<TicketEntity>>();

            if (!string.IsNullOrEmpty(message.UserId) && message.Metadata!.IsAdmin)
            {
                filters.Add(new FilterDefinitionBuilder<TicketEntity>().Eq(entity => entity.BuyerId, message.UserId));
            }
            else
            {
                filters.Add(new FilterDefinitionBuilder<TicketEntity>().Eq(entity => entity.BuyerId, message.Metadata!.UserId));
            }

            if (!string.IsNullOrEmpty(message.OrderId))
            {
                filters.Add(new FilterDefinitionBuilder<TicketEntity>().Eq(entity => entity.OrderId, message.OrderId));
            }

            if (!string.IsNullOrEmpty(message.Status))
            {
                filters.Add(new FilterDefinitionBuilder<TicketEntity>().Eq(entity => entity.Status, message.Status));
            }

            var entities = await _entityDataService.ListEntities<TicketEntity>(filter =>
                filters.Any() ? filter.And(filters) : FilterDefinition<TicketEntity>.Empty);

            return new GetTicketsGrpcCommandResult
            {
                Metadata = new GrpcCommandResultMetadata { Success = true },
                Data = _mapper.Map<TicketDto[]>(entities)
            };
        });
    }

    public ValueTask<GetTicketByIdGrpcCommandResult> GetTicketById(GetTicketByIdGrpcCommandMessage message)
    {
        return TryAsync(async () =>
        {
            var filters = new List<FilterDefinition<TicketEntity>>()
            {
                new FilterDefinitionBuilder<TicketEntity>().Eq(entity => entity.Id, message.Id)
            };

            if (!message.Metadata!.IsAdmin)
            {
                filters.Add(new FilterDefinitionBuilder<TicketEntity>().Eq(ticketEntity => ticketEntity.BuyerId, message.Metadata.UserId));
            }

            var entity = await _entityDataService.GetEntity<TicketEntity>(filter => filter.And(filters));

            return new GetTicketByIdGrpcCommandResult
            {
                Metadata = new GrpcCommandResultMetadata{ Success = true },
                Data = _mapper.Map<TicketDto>(entity)
            };
        });
    }

    public ValueTask<BookTicketsGrpcCommandResult> BookTickets(BookTicketsGrpcCommandMessage message)
    {
        return TryAsync(async () =>
        {
            await _publishEndpoint.Publish(new BookTicketsCommandMessage()
            {
                UserId = message.Metadata!.UserId,
                Bookings = message.Bookings
            });

            return new BookTicketsGrpcCommandResult{ Metadata = new GrpcCommandResultMetadata { Success = true } };
        });
    }
}