using App.Data.Services;
using App.Infrastructure.Events;
using App.Services.Events.Data.Entities;
using App.Services.Tournaments.Infrastructure.Events;
using MassTransit;
using Microsoft.IdentityModel.Tokens;
using MongoDB.Driver;

namespace App.Services.Events.Infrastructure.EventHandlers;

public class TournamentCreatedEventHandler : IEventHandler<TournamentCreatedEventMessage>
{
    private readonly IEntityDataService _entityDataService;

    public TournamentCreatedEventHandler(IEntityDataService entityDataService)
    {
        this._entityDataService = entityDataService;
    }

    public async Task Consume(ConsumeContext<TournamentCreatedEventMessage> context)
    {
        var @event = await this._entityDataService.GetEntity<EventEntity>(context.Message.EventId);

        var turnaments = new List<string>();
        if (!@event.Tournaments.IsNullOrEmpty()) turnaments = @event.Tournaments.ToList();
        turnaments.Add(context.Message.Id);

        var updateDefinition =
            new UpdateDefinitionBuilder<EventEntity>().Set(entity => entity.Tournaments, turnaments.ToArray());

        await this._entityDataService.Update<EventEntity>(filter => filter.Eq(enity => enity.Id, @event.Id),
            _ => updateDefinition);
    }
}