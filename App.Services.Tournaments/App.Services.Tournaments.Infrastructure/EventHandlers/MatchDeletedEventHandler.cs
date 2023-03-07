using App.Data.Services;
using App.Infrastructure.Events;
using App.Services.Tournaments.Data.Entities;
using App.Services.Tournaments.Infrastructure.Events;
using MassTransit;
using MongoDB.Driver;

namespace App.Services.Tournaments.Infrastructure.EventHandlers;

public class MatchDeletedEventHandler : IEventHandler<MatchDeletedEventMessage>
{
    private readonly IEntityDataService _entityDataService;

    public MatchDeletedEventHandler(IEntityDataService entityDataService)
    {
        _entityDataService = entityDataService;
    }

    public async Task Consume(ConsumeContext<MatchDeletedEventMessage> context)
    {
        var message = context.Message;

        await _entityDataService.Update<TournamentEntity>(
            filter => filter.AnyEq(entity => entity.MatchesId, message.Id),
            builder => builder.Pull(entity => entity.MatchesId, message.Id));
    }
}