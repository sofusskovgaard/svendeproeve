using App.Data.Services;
using App.Infrastructure.Events;
using App.Services.Games.Infrastructure.Events;
using App.Services.Tournaments.Data.Entities;
using MassTransit;
using MongoDB.Driver;

namespace App.Services.Tournaments.Infrastructure.EventHandlers;

public class GameDeletedEventHandler : IEventHandler<GameDeletedEventMessage>
{
    private readonly IEntityDataService _entityDataService;

    public GameDeletedEventHandler(IEntityDataService entityDataService)
    {
        _entityDataService = entityDataService;
    }

    public async Task Consume(ConsumeContext<GameDeletedEventMessage> context)
    {
        var message = context.Message;

        var turnaments =
            await _entityDataService.ListEntities<TournamentEntity>(filter =>
                filter.Eq(entity => entity.GameId, message.Id));

        foreach (var turnament in turnaments)
        {
            var updateDefinition = new UpdateDefinitionBuilder<TournamentEntity>().Set(entity => entity.GameId, null);

            await _entityDataService.Update<TournamentEntity>(filter => filter.Eq(entity => entity.Id, turnament.Id),
                _ => updateDefinition);
        }
    }
}