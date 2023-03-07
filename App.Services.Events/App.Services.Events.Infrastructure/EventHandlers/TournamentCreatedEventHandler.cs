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

        var tournaments = new List<string>();

        if (!@event.Tournaments.IsNullOrEmpty())
            tournaments = @event.Tournaments.ToList();

        tournaments.Add(context.Message.Id);

        await this._entityDataService.Update<EventEntity>(filter => filter.Eq(entity => entity.Id, @event.Id),
            builder => builder.Set(entity => entity.Tournaments, tournaments.ToArray()));
    }
}