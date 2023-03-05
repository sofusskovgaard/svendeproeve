using App.Data.Services;
using App.Infrastructure.Commands;
using App.Services.Events.Data.Entities;
using App.Services.Events.Infrastructure.Commands;
using App.Services.Events.Infrastructure.Events;
using MassTransit;

namespace App.Services.Events.Infrastructure.CommandHandlers;

public class DeleteEventCommandHandler : ICommandHandler<DeleteEventCommandMessage>
{
    private readonly IEntityDataService _entityDataService;

    private readonly IPublishEndpoint _publishEndpoint;

    public DeleteEventCommandHandler(IEntityDataService entityDataService, IPublishEndpoint publishEndpoint)
    {
        this._entityDataService = entityDataService;
        this._publishEndpoint = publishEndpoint;
    }

    public async Task Consume(ConsumeContext<DeleteEventCommandMessage> context)
    {
        var message = context.Message;

        await this._entityDataService.Delete<EventEntity>(filter => filter.Eq(entity => entity.Id, message.Id));

        await this._publishEndpoint.Publish(new EventDeletedEventMessage { Id = message.Id });
    }
}