using App.Data.Services;
using App.Infrastructure.Commands;
using App.Services.Authentication.Data.Entities;
using App.Services.Authentication.Infrastructure.Commands;
using App.Services.Authentication.Infrastructure.Events;
using MassTransit;

namespace App.Services.Authentication.Infrastructure.CommandHandlers;

public class ChangeEmailCommandHandler : ICommandHandler<ChangeEmailCommandMessage>
{
    private readonly IEntityDataService _entityDataService;

    public ChangeEmailCommandHandler(IEntityDataService entityDataService)
    {
        _entityDataService = entityDataService;
    }

    public async Task Consume(ConsumeContext<ChangeEmailCommandMessage> context)
    {
        var message = context.Message;

        var result = await _entityDataService.Update<UserLoginEntity>(
            filter => filter.Eq(entity => entity.Id, message.UserId),
            builder => builder.Set(entity => entity.Email, message.Email));

        if (result)
        {
            await context.Publish(new UserEmailChangedEventMessage { UserId = message.UserId, Email = message.Email });
        }
    }
}