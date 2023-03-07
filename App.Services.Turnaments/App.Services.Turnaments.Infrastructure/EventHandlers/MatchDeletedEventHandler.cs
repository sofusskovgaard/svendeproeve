using App.Data.Services;
using App.Infrastructure.Events;
using App.Services.Turnaments.Data.Entities;
using App.Services.Turnaments.Infrastructure.Events;
using MassTransit;
using MongoDB.Driver;

namespace App.Services.Turnaments.Infrastructure.EventHandlers;

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

        var turnaments = await _entityDataService.ListEntities<TurnamentEntity>(filter =>
            filter.AnyStringIn(entity => entity.MatchesId, message.Id));

        foreach (var turnament in turnaments)
        {
            turnament.MatchesId = turnament.MatchesId.Where(m => m != message.Id).ToArray();

            var updateDefinition =
                new UpdateDefinitionBuilder<TurnamentEntity>().Set(entity => entity.MatchesId, turnament.MatchesId);

            await _entityDataService.Update<TurnamentEntity>(filter => filter.Eq(entity => entity.Id, turnament.Id),
                _ => updateDefinition);
        }
    }
}