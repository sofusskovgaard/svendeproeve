using App.Data.Services;
using App.Infrastructure.Events;
using App.Services.Turnaments.Data.Entities;
using App.Services.Turnaments.Infrastructure.Events;
using MassTransit;
using Microsoft.IdentityModel.Tokens;
using MongoDB.Driver;

namespace App.Services.Turnaments.Infrastructure.EventHandlers
{
    public class MatchCreatedEventHandler : IEventHandler<MatchCreatedEventMessage>
    {
        private readonly IEntityDataService _entityDataService;

        public MatchCreatedEventHandler(IEntityDataService entityDataService)
        {
            _entityDataService = entityDataService;
        }

        public async Task Consume(ConsumeContext<MatchCreatedEventMessage> context)
        {
            var message = context.Message;

            var turnament = await _entityDataService.GetEntity<TurnamentEntity>(message.TurnamentId);

            List<string> matches = new List<string>();
            if (!turnament.MatchesId.IsNullOrEmpty())
            {
                matches = turnament.MatchesId.ToList();
            }
            matches.Add(message.Id);

            var updateDefinition = new UpdateDefinitionBuilder<TurnamentEntity>().Set(entity => entity.MatchesId, matches.ToArray());

            await _entityDataService.Update<TurnamentEntity>(filter => filter.Eq(entity => entity.Id, message.TurnamentId), _ => updateDefinition);
        }
    }
}
