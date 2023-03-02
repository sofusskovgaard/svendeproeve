using App.Data.Services;
using App.Infrastructure.Commands;
using App.Services.Turnaments.Data.Entities;
using App.Services.Turnaments.Infrastructure.Commands;
using App.Services.Turnaments.Infrastructure.Events;
using MassTransit;

namespace App.Services.Turnaments.Infrastructure.CommandHandlers
{
    public class DeleteTurnamentCommandHandler : ICommandHandler<DeleteTurnamentCommandMessage>
    {
        private readonly IEntityDataService _entityDataService;
        private readonly IPublishEndpoint _publishEndpoint;

        public DeleteTurnamentCommandHandler(IEntityDataService entityDataService, IPublishEndpoint publishEndpoint)
        {
            _entityDataService = entityDataService;
            _publishEndpoint = publishEndpoint;
        }

        public async Task Consume(ConsumeContext<DeleteTurnamentCommandMessage> context)
        {
            var message = context.Message;

            var turnament = await _entityDataService.GetEntity<TurnamentEntity>(message.Id);

            await _entityDataService.Delete<TurnamentEntity>(turnament);

            await _publishEndpoint.Publish(new TurnamentDeletedEventMessage
            {
                Id = message.Id
            });
        }
    }
}
