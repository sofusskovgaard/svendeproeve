using App.Data.Services;
using App.Infrastructure.Events;
using App.Services.Events.Data.Entities;
using App.Services.Tournaments.Infrastructure.Events;
using MassTransit;
using MongoDB.Driver;

namespace App.Services.Events.Infrastructure.EventHandlers;

public class TournamentDeletedEventHandler : IEventHandler<TournamentDeletedEventMessage>
{
    private readonly IEntityDataService _entityDataService;

    public TournamentDeletedEventHandler(IEntityDataService entityDataService)
    {
        this._entityDataService = entityDataService;
    }

    public async Task Consume(ConsumeContext<TournamentDeletedEventMessage> context)
    {
        var message = context.Message;

        var events = await this._entityDataService.ListEntities<EventEntity>(filter =>
            filter.AnyStringIn(entity => entity.Tournaments, message.Id));

        foreach (var @event in events)
            if (@event.Tournaments.Contains(message.Id))
            {
                @event.Tournaments = @event.Tournaments.Where(t => t != message.Id).ToArray();

                var updateDefinition =
                    new UpdateDefinitionBuilder<EventEntity>().Set(entity => entity.Tournaments, @event.Tournaments);

                await this._entityDataService.Update<EventEntity>(filter => filter.Eq(entity => entity.Id, @event.Id),
                    _ => updateDefinition);
            }
    }
}