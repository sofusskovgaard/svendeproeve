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
            var message = context.Message;

            await _entityDataService.Update<UserEntity>(filter => filter.AnyEq(entity => entity.Teams, message.Id),
                builder => builder.Pull(entity => entity.Teams, message.Id));
        }
    }
}
