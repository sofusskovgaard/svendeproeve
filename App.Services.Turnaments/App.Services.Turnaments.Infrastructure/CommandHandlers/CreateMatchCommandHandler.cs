using App.Data.Services;
using App.Infrastructure.Commands;
using App.Services.Turnaments.Data.Entities;
using App.Services.Turnaments.Infrastructure.Commands;
using MassTransit;

namespace App.Services.Turnaments.Infrastructure.CommandHandlers
{
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

            MatchEntity match = new MatchEntity
            {
                Name = message.Name,
                TeamsId = message.TeamsId,
                TurnamentId = message.TurnamentId
            };

            match = await this._entityDataService.Create<MatchEntity>(match);
        }
    }
}
