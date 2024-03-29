﻿using App.Data.Services;
using App.Infrastructure.Events;
using App.Services.Teams.Infrastructure.Events;
using App.Services.Users.Data.Entities;
using MassTransit;
using MongoDB.Driver;

namespace App.Services.Users.Infrastructure.EventHandlers
{
    public class TeamCreatedEventHandler : IEventHandler<TeamCreatedEventMessage>
    {
        private readonly IEntityDataService _entityDataService;

        public TeamCreatedEventHandler(IEntityDataService entityDataService)
        {
            _entityDataService = entityDataService;
        }

        public async Task Consume(ConsumeContext<TeamCreatedEventMessage> context)
        {
            var users = await _entityDataService.ListEntities<UserEntity>();

            foreach (var user in users)
            {
                if (context.Message.UsersId.Contains(user.Id))
                {
                    List<string> teams = new List<string>();
                    teams = user.Teams.ToList();
                    teams.Add(context.Message.Id);

                    var updateDefinition = new UpdateDefinitionBuilder<UserEntity>().Set(entity => entity.Teams, teams.ToArray());

                    if (!user.IsInTeam)
                    {
                        updateDefinition = updateDefinition.Set(entity => entity.IsInTeam, true);
                    }

                    await _entityDataService.Update<UserEntity>(filter => filter.Eq(entity => entity.Id, user.Id), _ => updateDefinition);
                }
            }
        }
    }
}
