using App.Data.Services;
using App.Infrastructure.Commands;
using App.Infrastructure.Utilities;
using App.Services.Authentication.Data.Entities;
using App.Services.Authentication.Infrastructure.Commands;
using App.Services.Authentication.Infrastructure.Events;
using App.Services.Authentication.Infrastructure.Services;
using MassTransit;
using MongoDB.Driver;

namespace App.Services.Authentication.Infrastructure.CommandHandlers;

public class ChangePasswordCommandHandler : ICommandHandler<ChangePasswordCommandMessage>
{
    private readonly IEntityDataService _entityDataService;

    public ChangePasswordCommandHandler(IEntityDataService entityDataService)
    {
        _entityDataService = entityDataService;
    }

    public async Task Consume(ConsumeContext<ChangePasswordCommandMessage> context)
    {
        var message = context.Message;

        var hashResponse = Hasher.Hash(message.Password);

        var result = await _entityDataService.Update<UserLoginEntity>(
            filter => filter.Eq(entity => entity.Id, message.UserId),
            builder => builder
                .Set(entity => entity.PasswordHash, hashResponse.Hash)
                .Set(entity => entity.PasswordSalt, hashResponse.Salt));

        if (result)
        {
            await context.Publish(new UserPasswordChangedEventMessage { UserId = message.UserId });
        }
    }
}