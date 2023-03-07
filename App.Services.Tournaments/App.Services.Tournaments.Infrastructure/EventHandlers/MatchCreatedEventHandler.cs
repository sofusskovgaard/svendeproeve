using App.Data.Services;
using App.Infrastructure.Events;
using App.Services.Tournaments.Data.Entities;
using App.Services.Tournaments.Infrastructure.Events;
using MassTransit;
using Microsoft.IdentityModel.Tokens;
using MongoDB.Driver;

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

        var turnament = await _entityDataService.GetEntity<TournamentEntity>(message.TurnamentId);

        var matches = new List<string>();
        if (!turnament.MatchesId.IsNullOrEmpty()) matches = turnament.MatchesId.ToList();
        matches.Add(message.Id);

        var updateDefinition =
            new UpdateDefinitionBuilder<TournamentEntity>().Set(entity => entity.MatchesId, matches.ToArray());

        await _entityDataService.Update<TournamentEntity>(filter => filter.Eq(entity => entity.Id, message.TurnamentId),
            _ => updateDefinition);
    }
}