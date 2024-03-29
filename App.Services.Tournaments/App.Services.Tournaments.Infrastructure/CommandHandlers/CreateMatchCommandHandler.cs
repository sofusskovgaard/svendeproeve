﻿using App.Data.Services;
using App.Infrastructure.Commands;
using App.Services.Tournaments.Data.Entities;
using App.Services.Tournaments.Infrastructure.Commands;
using App.Services.Tournaments.Infrastructure.Events;
using MassTransit;

namespace App.Services.Tournaments.Infrastructure.CommandHandlers;

public class CreateMatchCommandHandler : ICommandHandler<CreateMatchCommandMessage>
{
    private readonly IEntityDataService _entityDataService;

    private readonly IPublishEndpoint _publishEndpoint;

    public CreateMatchCommandHandler(IEntityDataService entityDataService, IPublishEndpoint publishEndpoint)
    {
        _entityDataService = entityDataService;
        _publishEndpoint = publishEndpoint;
    }

    public async Task Consume(ConsumeContext<CreateMatchCommandMessage> context)
    {
        var message = context.Message;

        var match = new MatchEntity
        {
            Name = message.Name,
            TeamsId = message.TeamsId,
            TournamentId = message.TournamentId
        };

        match = await _entityDataService.Create(match);

        await _publishEndpoint.Publish(new MatchCreatedEventMessage
        {
            Id = match.Id,
            TournamentId = message.TournamentId
        });
    }
}