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

        await _entityDataService.Update<TeamEntity>(filter => filter.Eq(entity => entity.GameId, message.Id),
            builder => builder.Unset(entity => entity.GameId));
    }
}