using App.Data.Services;
using App.Infrastructure.Commands;
using App.Services.Users.Data.Entities;
using App.Services.Users.Infrastructure.Commands;
using App.Services.Users.Infrastructure.Events;
using MassTransit;
using Microsoft.Extensions.Logging;

namespace App.Services.Users.Infrastructure.CommandHandlers;

public class CreateUserCommandHandler : ICommandHandler<CreateUserCommandMessage>
{
    private readonly IEntityDataService _entityDataService;
    private readonly ILogger<CreateUserCommandHandler> _logger;

    public CreateUserCommandHandler(ILogger<CreateUserCommandHandler> logger, IEntityDataService entityDataService)
    {
        _logger = logger;
        _entityDataService = entityDataService;
    }

    public async Task Consume(ConsumeContext<CreateUserCommandMessage> context)
    {
        var message = context.Message;

        var user = new UserEntity
        {
            Firstname = message.Firstname,
            Lastname = message.Lastname,
            Username = message.Username,
            Email = message.Email
        };

        if (!string.IsNullOrEmpty(message.Id)) user.Id = message.Id;

        await _entityDataService.SaveEntity(user);

        _logger.LogInformation("created user with id: {id}", user.Id);

        await context.Publish(new UserCreatedEventMessage
        {
            Id = user.Id,
            ConnectionId = message.ConnectionId
        });
    }
}