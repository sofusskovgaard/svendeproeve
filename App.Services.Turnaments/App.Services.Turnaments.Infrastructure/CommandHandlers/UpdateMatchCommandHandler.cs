using App.Data.Services;
using App.Infrastructure.Commands;
using App.Services.Turnaments.Data.Entities;
using App.Services.Turnaments.Infrastructure.Commands;
using MassTransit;
using Microsoft.IdentityModel.Tokens;
using MongoDB.Driver;

namespace App.Services.Turnaments.Infrastructure.CommandHandlers
{
    public class UpdateMatchCommandHandler : ICommandHandler<UpdateMatchCommandMessage>
    {
        private readonly IEntityDataService _entityDataService;

        public UpdateMatchCommandHandler(IEntityDataService entityDataService)
        {
            _entityDataService = entityDataService;
        }

        public async Task Consume(ConsumeContext<UpdateMatchCommandMessage> context)
        {
            var message = context.Message;

            var match = await _entityDataService.GetEntity<MatchEntity>(message.Id);

            var updateDefinition = new UpdateDefinitionBuilder<MatchEntity>().Set(entity => entity.Name, message.Name);

            if (!message.WinningTeamId.IsNullOrEmpty())
            {
                if (match.WinningTeamId != message.WinningTeamId)
                {
                    updateDefinition = updateDefinition.Set(entity => entity.WinningTeamId, message.WinningTeamId);
                }
            }

            await _entityDataService.Update<MatchEntity>(filter => filter.Eq(entity => entity.Id, message.Id), _ => updateDefinition);
        }
    }
}
