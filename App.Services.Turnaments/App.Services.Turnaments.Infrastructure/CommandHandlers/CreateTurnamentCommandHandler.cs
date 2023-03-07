using App.Data.Services;
using App.Infrastructure.Commands;
using App.Services.Turnaments.Data.Entities;
using App.Services.Turnaments.Infrastructure.Commands;
using App.Services.Turnaments.Infrastructure.Events;
using MassTransit;

namespace App.Services.Turnaments.Infrastructure.CommandHandlers;

public class CreateTurnamentCommandHandler : ICommandHandler<CreateTurnamentCommandMessage>
{
    private readonly IEntityDataService _entityDataService;

    private readonly IPublishEndpoint _publishEndpoint;

    public CreateTurnamentCommandHandler(IEntityDataService entityDataService, IPublishEndpoint publishEndpoint)
    {
        _entityDataService = entityDataService;
        _publishEndpoint = publishEndpoint;
    }

    public async Task Consume(ConsumeContext<CreateTurnamentCommandMessage> context)
    {
        var message = context.Message;

        var turnament = new TurnamentEntity
        {
            Name = message.Name,
            GameId = message.GameId,
            EventId = message.EventId
        };

        turnament = await _entityDataService.Create(turnament);

        await _publishEndpoint.Publish(new TurnamentCreatedEventMessage
        {
            Id = turnament.Id,
            EventId = message.EventId
        });
    }
}