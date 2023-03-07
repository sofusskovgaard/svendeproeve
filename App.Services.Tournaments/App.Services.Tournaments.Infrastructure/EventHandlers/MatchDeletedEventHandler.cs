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

        var turnaments = await _entityDataService.ListEntities<TournamentEntity>(filter =>
            filter.AnyStringIn(entity => entity.MatchesId, message.Id));

        foreach (var turnament in turnaments)
        {
            turnament.MatchesId = turnament.MatchesId.Where(m => m != message.Id).ToArray();

            var updateDefinition =
                new UpdateDefinitionBuilder<TournamentEntity>().Set(entity => entity.MatchesId, turnament.MatchesId);

            await _entityDataService.Update<TournamentEntity>(filter => filter.Eq(entity => entity.Id, turnament.Id),
                _ => updateDefinition);
        }
    }
}