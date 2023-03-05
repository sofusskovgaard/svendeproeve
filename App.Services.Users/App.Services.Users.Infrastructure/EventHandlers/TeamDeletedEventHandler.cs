using App.Data.Services;
using App.Infrastructure.Events;
using App.Services.Teams.Infrastructure.Events;
using App.Services.Users.Data.Entities;
using MassTransit;
using MongoDB.Driver;

namespace App.Services.Users.Infrastructure.EventHandlers
{
    public class TeamDeletedEventHandler : IEventHandler<TeamDeletedEventMessage>
    {
        private readonly IEntityDataService _entityDataService;

        public TeamDeletedEventHandler(IEntityDataService entityDataService)
        {
            _entityDataService = entityDataService;
        }

        public async Task Consume(ConsumeContext<TeamDeletedEventMessage> context)
        {
            //var users1 = await _entityDataService.ListEntities(new ExpressionFilterDefinition<UserEntity>(entity => entity.Teams.Contains(context.Message.Id)));
            var users = await _entityDataService.ListEntities<UserEntity>(filter => filter.AnyStringIn(entity => entity.Teams, context.Message.Id));

            foreach (var user in users)
            {
                user.Teams = user.Teams.Where(t => t != context.Message.Id).ToArray();

                var updateDefinition = new UpdateDefinitionBuilder<UserEntity>().Set(entity => entity.Teams, user.Teams);

                if (!user.Teams.Any())
                {
                    updateDefinition = updateDefinition.Set(entity => entity.IsInTeam, false);
                }

                await _entityDataService.Update<UserEntity>(filter => filter.Eq(entity => entity.Id, user.Id), _ => updateDefinition);
            }
        }
    }
}
