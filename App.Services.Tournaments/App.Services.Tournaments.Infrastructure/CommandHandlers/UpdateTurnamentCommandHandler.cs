using App.Data.Services;
using App.Infrastructure.Commands;
using App.Services.Tournaments.Data.Entities;
using App.Services.Tournaments.Infrastructure.Commands;
using MassTransit;
using MongoDB.Driver;

namespace App.Services.Tournaments.Infrastructure.CommandHandlers;

public class UpdateTurnamentCommandHandler : ICommandHandler<UpdateTournamentCommandMessage>
{
    private readonly IEntityDataService _entityDataService;

    public UpdateTurnamentCommandHandler(IEntityDataService entityDataService)
    {
        _entityDataService = entityDataService;
    }

    public async Task Consume(ConsumeContext<UpdateTournamentCommandMessage> context)
    {
        var message = context.Message;

        var turnament = await _entityDataService.GetEntity<TournamentEntity>(message.Id);

        var updateDefinition = new UpdateDefinitionBuilder<TournamentEntity>().Set(entity => entity.Name, message.Name);

        if (message.GameId != turnament.GameId)
            updateDefinition = updateDefinition.Set(entity => entity.GameId, message.GameId);

        await _entityDataService.Update<TournamentEntity>(filter => filter.Eq(entity => entity.Id, message.Id),
            _ => updateDefinition);
    }
}