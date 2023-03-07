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

        await _entityDataService.Update<EventEntity>(filter => filter.AnyEq(entity => entity.Tournaments, message.Id),
            builder => builder.Pull(entity => entity.Tournaments, message.Id));
    }
}