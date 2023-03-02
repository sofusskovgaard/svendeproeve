using App.Data.Services;
using App.Infrastructure.Events;
using App.Services.Authentication.Infrastructure.Events;
using App.Services.Users.Data.Entities;
using MassTransit;

namespace App.Services.Users.Infrastructure.EventHandlers;

public class UserUsernameChangedEventHandler : IEventHandler<UserUsernameChangedEventMessage>
{
    private readonly IEntityDataService _entityDataService;

    public UserUsernameChangedEventHandler(IEntityDataService entityDataService)
    {
        _entityDataService = entityDataService;
    }

    public async Task Consume(ConsumeContext<UserUsernameChangedEventMessage> context)
    {
        var message = context.Message;

        await _entityDataService.Update<UserEntity>(
            filter => filter.Eq(entity => entity.Id, message.UserId),
            builder => builder.Set(entity => entity.Username, message.Username));
    }
}