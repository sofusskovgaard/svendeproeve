using App.Data.Services;
using App.Infrastructure.Events;
using App.Services.Games.Infrastructure.Events;
using App.Services.Teams.Data.Entities;
using MassTransit;
using MongoDB.Driver;

namespace App.Services.Teams.Infrastructure.EventHandlers;

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

        var teams = await _entityDataService.ListEntities<TeamEntity>(filter =>
            filter.Eq(entity => entity.GameId, message.Id));

        foreach (var team in teams)
        {
            var updateDefinition = new UpdateDefinitionBuilder<TeamEntity>().Set(entity => entity.GameId, null);

            await _entityDataService.Update<TeamEntity>(filter => filter.Eq(entity => entity.Id, team.Id),
                _ => updateDefinition);
        }
    }
}