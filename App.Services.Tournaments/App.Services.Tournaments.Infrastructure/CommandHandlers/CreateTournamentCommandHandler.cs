using App.Data.Services;
using App.Infrastructure.Commands;
using App.Services.Tournaments.Data.Entities;
using App.Services.Tournaments.Infrastructure.Commands;
using App.Services.Tournaments.Infrastructure.Events;
using MassTransit;

namespace App.Services.Tournaments.Infrastructure.CommandHandlers;

public class CreateTournamentCommandHandler : ICommandHandler<CreateTournamentCommandMessage>
{
    private readonly IEntityDataService _entityDataService;

    private readonly IPublishEndpoint _publishEndpoint;

    public CreateTournamentCommandHandler(IEntityDataService entityDataService, IPublishEndpoint publishEndpoint)
    {
        _entityDataService = entityDataService;
        _publishEndpoint = publishEndpoint;
    }

    public async Task Consume(ConsumeContext<CreateTournamentCommandMessage> context)
    {
        var message = context.Message;

        var entity = await _entityDataService.Create(new TournamentEntity
        {
            Name = message.Name,
            GameId = message.GameId,
            EventId = message.EventId
        });

        await _publishEndpoint.Publish(new TournamentCreatedEventMessage{ Id = entity.Id!, EventId = message.EventId });
    }
}