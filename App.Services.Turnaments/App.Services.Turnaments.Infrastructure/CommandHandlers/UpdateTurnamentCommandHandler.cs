using App.Data.Services;
using App.Infrastructure.Commands;
using App.Services.Turnaments.Data.Entities;
using App.Services.Turnaments.Infrastructure.Commands;
using MassTransit;
using MongoDB.Driver;

namespace App.Services.Turnaments.Infrastructure.CommandHandlers
{
    public class UpdateTurnamentCommandHandler : ICommandHandler<UpdateTurnamentCommandMessage>
    {
        private readonly IEntityDataService _entityDataService;

        public UpdateTurnamentCommandHandler(IEntityDataService entityDataService)
        {
            _entityDataService = entityDataService;
        }

        public async Task Consume(ConsumeContext<UpdateTurnamentCommandMessage> context)
        {
            var message = context.Message;

            var turnament = await _entityDataService.GetEntity<TurnamentEntity>(message.Id);

            var updateDefinition = new UpdateDefinitionBuilder<TurnamentEntity>().Set(entity => entity.Name, message.Name);

            if (message.GameId != turnament.GameId)
            {
                updateDefinition = updateDefinition.Set(entity => entity.GameId, message.GameId);
            }

            await _entityDataService.Update<TurnamentEntity>(filter => filter.Eq(entity => entity.Id, message.Id), _ => updateDefinition);
        }
    }
}
