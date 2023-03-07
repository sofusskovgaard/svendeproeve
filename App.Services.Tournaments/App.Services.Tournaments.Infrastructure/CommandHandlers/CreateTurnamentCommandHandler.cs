using App.Data.Services;
using App.Infrastructure.Commands;
using App.Services.Tournaments.Data.Entities;
using App.Services.Tournaments.Infrastructure.Commands;
using App.Services.Tournaments.Infrastructure.Events;
using MassTransit;

namespace App.Services.Tournaments.Infrastructure.CommandHandlers;

public class CreateTurnamentCommandHandler : ICommandHandler<CreateTournamentCommandMessage>
{
    private readonly IEntityDataService _entityDataService;

    private readonly IPublishEndpoint _publishEndpoint;

    public CreateTurnamentCommandHandler(IEntityDataService entityDataService, IPublishEndpoint publishEndpoint)
    {
        _entityDataService = entityDataService;
        _publishEndpoint = publishEndpoint;
    }

    public async Task Consume(ConsumeContext<CreateTournamentCommandMessage> context)
    {
        var message = context.Message;

        var turnament = new TournamentEntity
        {
            Name = message.Name,
            GameId = message.GameId,
            EventId = message.EventId
        };

        turnament = await _entityDataService.Create(turnament);

        await _publishEndpoint.Publish(new TournamentCreatedEventMessage
        {
            Id = turnament.Id,
            EventId = message.EventId
        });
    }
}