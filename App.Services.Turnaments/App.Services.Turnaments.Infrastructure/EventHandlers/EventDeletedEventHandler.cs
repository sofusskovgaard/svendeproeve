using App.Data.Services;
using App.Infrastructure.Events;
using App.Services.Events.Infrastructure.Events;
using App.Services.Turnaments.Data.Entities;
using MassTransit;
using MongoDB.Driver;

namespace App.Services.Turnaments.Infrastructure.EventHandlers;

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
            await _entityDataService.ListEntities<TurnamentEntity>(filter =>
                filter.Eq(entity => entity.EventId, message.Id));

        foreach (var turnament in turnaments)
        {
            var updateDefinition = new UpdateDefinitionBuilder<TurnamentEntity>().Set(entity => entity.EventId, null);

            await _entityDataService.Update<TurnamentEntity>(filter => filter.Eq(entity => entity.Id, turnament.Id),
                _ => updateDefinition);
        }
    }
}