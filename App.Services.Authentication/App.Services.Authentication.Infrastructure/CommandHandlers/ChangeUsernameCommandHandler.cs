using App.Data.Services;
using App.Infrastructure.Commands;
using App.Services.Authentication.Data.Entities;
using App.Services.Authentication.Infrastructure.Commands;
using App.Services.Authentication.Infrastructure.Events;
using MassTransit;

namespace App.Services.Authentication.Infrastructure.CommandHandlers;

public class ChangeUsernameCommandHandler : ICommandHandler<ChangeUsernameCommandMessage>
{
    private readonly IEntityDataService _entityDataService;

    public ChangeUsernameCommandHandler(IEntityDataService entityDataService)
    {
        _entityDataService = entityDataService;
    }

    public async Task Consume(ConsumeContext<ChangeUsernameCommandMessage> context)
    {
        var message = context.Message;

        var result = await _entityDataService.Update<UserLoginEntity>(filter => filter.Eq(entity => entity.Id, message.UserId),
            builder => builder.Set(entity => entity.Username, message.Username));

        if (result)
        {
            await context.Publish(
                new UserUsernameChangedEventMessage { UserId = message.UserId, Username = message.Username });
        }
    }
}