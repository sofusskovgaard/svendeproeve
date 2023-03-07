using App.Data.Services;
using App.Infrastructure.Events;
using App.Services.Teams.Infrastructure.Events;
using App.Services.Tournaments.Data.Entities;
using MassTransit;
using MongoDB.Driver;

namespace App.Services.Tournaments.Infrastructure.EventHandlers;

public class TeamDeletedEventHandler : IEventHandler<TeamDeletedEventMessage>
{
    private readonly IEntityDataService _entityDataService;

    public TeamDeletedEventHandler(IEntityDataService entityDataService)
    {
        _entityDataService = entityDataService;
    }

    public async Task Consume(ConsumeContext<TeamDeletedEventMessage> context)
    {
        var message = context.Message;

        var matches =
            await _entityDataService.ListEntities<MatchEntity>(filter =>
                filter.AnyStringIn(entity => entity.TeamsId, message.Id));

        foreach (var match in matches)
        {
            match.TeamsId = match.TeamsId.Where(t => t != message.Id).ToArray();

            var updateDefinition =
                new UpdateDefinitionBuilder<MatchEntity>().Set(entity => entity.TeamsId, match.TeamsId);

            await _entityDataService.Update<MatchEntity>(filter => filter.Eq(entity => entity.Id, match.Id),
                _ => updateDefinition);
        }
    }
}