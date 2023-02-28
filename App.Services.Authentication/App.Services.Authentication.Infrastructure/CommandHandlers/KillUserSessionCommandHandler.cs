using App.Data.Services;
using App.Infrastructure.Commands;
using App.Services.Authentication.Data.Entities;
using App.Services.Authentication.Infrastructure.Commands;
using App.Services.Authentication.Infrastructure.Events;
using MassTransit;
using MongoDB.Driver;

namespace App.Services.Authentication.Infrastructure.CommandHandlers
{
    public class KillUserSessionCommandHandler : ICommandHandler<KillUserSessionCommandMessage>
    {
        private readonly IEntityDataService _entityDataService;

        public KillUserSessionCommandHandler(IEntityDataService entityDataService)
        {
            _entityDataService = entityDataService;
        }

        public async Task Consume(ConsumeContext<KillUserSessionCommandMessage> context)
        {
            var message = context.Message;

            var result = await _entityDataService.Delete<UserSessionEntity>(filter =>
                filter.And(filter.Eq(entity => entity.Id, message.SessionId),
                    filter.Eq(entity => entity.UserId, message.UserId)));

            if (result)
            {
                await context.Publish(new UserSessionKilledEventMessage()
                {
                    SessionId = context.Message.SessionId,
                    UserId = context.Message.UserId
                });
            }
        }
    }   
}