using App.Data.Services;
using App.Infrastructure.Commands;
using App.Services.Tournaments.Data.Entities;
using App.Services.Tournaments.Infrastructure.Commands;
using MassTransit;
using MongoDB.Driver;

namespace App.Services.Tournaments.Infrastructure.CommandHandlers;

public class UpdateTournamentCommandHandler : ICommandHandler<UpdateTournamentCommandMessage>
{
    private readonly IEntityDataService _entityDataService;

    public UpdateTournamentCommandHandler(IEntityDataService entityDataService)
    {
        _entityDataService = entityDataService;
    }

    public async Task Consume(ConsumeContext<UpdateTournamentCommandMessage> context)
    {
        var message = context.Message;

        await _entityDataService.Update<TournamentEntity>(filter => filter.Eq(entity => entity.Id, message.Id),
            builder => builder.Set(entity => entity.Name, message.Name).Set(entity => entity.GameId, message.GameId));
    }
}