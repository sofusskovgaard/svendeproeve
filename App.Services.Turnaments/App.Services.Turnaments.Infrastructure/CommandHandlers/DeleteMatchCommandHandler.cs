using App.Data.Services;
using App.Infrastructure.Commands;
using App.Services.Turnaments.Data.Entities;
using App.Services.Turnaments.Infrastructure.Commands;
using App.Services.Turnaments.Infrastructure.Events;
using MassTransit;

namespace App.Services.Turnaments.Infrastructure.CommandHandlers;

public class DeleteMatchCommandHandler : ICommandHandler<DeleteMatchCommandMessage>
{
    private readonly IEntityDataService _entityDataService;

    private readonly IPublishEndpoint _publishEndpoint;

    public DeleteMatchCommandHandler(IEntityDataService entityDataService, IPublishEndpoint publishEndpoint)
    {
        _entityDataService = entityDataService;
        _publishEndpoint = publishEndpoint;
    }

    public async Task Consume(ConsumeContext<DeleteMatchCommandMessage> context)
    {
        var message = context.Message;

        var match = await _entityDataService.GetEntity<MatchEntity>(message.Id);

        await _entityDataService.Delete(match);

        await _publishEndpoint.Publish(new MatchDeletedEventMessage
        {
            Id = message.Id
        });
    }
}