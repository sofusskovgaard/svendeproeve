using App.Data.Services;
using App.Infrastructure.Events;
using App.Services.Events.Infrastructure.Events;
using App.Services.Tournaments.Data.Entities;
using MassTransit;
using MongoDB.Driver;

namespace App.Services.Tournaments.Infrastructure.EventHandlers;

public class EventDeletedEventHandler : IEventHandler<EventDeletedEventMessage>
{
    private readonly IEntityDataService _entityDataService;

    public EventDeletedEventHandler(IEntityDataService entityDataService)
    {
        _entityDataService = entityDataService;
    }

    public async Task Consume(ConsumeContext<EventDeletedEventMessage> context)
    {
        var message = context.Message;

        var turnaments =
            await _entityDataService.ListEntities<TournamentEntity>(filter =>
                filter.Eq(entity => entity.EventId, message.Id));

        foreach (var turnament in turnaments)
        {
            var updateDefinition = new UpdateDefinitionBuilder<TournamentEntity>().Set(entity => entity.EventId, null);

            await _entityDataService.Update<TournamentEntity>(filter => filter.Eq(entity => entity.Id, turnament.Id),
                _ => updateDefinition);
        }
    }
}