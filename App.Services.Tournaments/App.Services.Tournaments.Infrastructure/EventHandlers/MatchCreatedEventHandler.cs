using App.Data.Services;
using App.Infrastructure.Events;
using App.Services.Tournaments.Data.Entities;
using App.Services.Tournaments.Infrastructure.Events;
using MassTransit;

namespace App.Services.Tournaments.Infrastructure.EventHandlers;

public class MatchCreatedEventHandler : IEventHandler<MatchCreatedEventMessage>
{
    private readonly IEntityDataService _entityDataService;

    public MatchCreatedEventHandler(IEntityDataService entityDataService)
    {
        _entityDataService = entityDataService;
    }

    public async Task Consume(ConsumeContext<MatchCreatedEventMessage> context)
    {
        var message = context.Message;

        await _entityDataService.Update<TournamentEntity>(
            filter => filter.Eq(entity => entity.Id, message.TournamentId),
            builder => builder.AddToSet(entity => entity.MatchesId, message.Id));
    }
}