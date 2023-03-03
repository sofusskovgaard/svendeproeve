using App.Data.Services;
using App.Infrastructure.Events;
using App.Services.Events.Data.Entities;
using App.Services.Turnaments.Infrastructure.Events;
using MassTransit;
using Microsoft.IdentityModel.Tokens;
using MongoDB.Driver;

namespace App.Services.Events.Infrastructure.EventHandlers
{
    public class TurnamentCreatedEventHandler : IEventHandler<TurnamentCreatedEventMessage>
    {
        private readonly IEntityDataService _entityDataService;

        public TurnamentCreatedEventHandler(IEntityDataService entityDataService)
        {
            _entityDataService = entityDataService;
        }

        public async Task Consume(ConsumeContext<TurnamentCreatedEventMessage> context)
        {
            var @event = await _entityDataService.GetEntity<EventEntity>(context.Message.EventId);

            List<string> turnaments = new List<string>();
            if (!@event.Tournaments.IsNullOrEmpty())
            {
                turnaments = @event.Tournaments.ToList();
            }
            turnaments.Add(context.Message.Id);

            var updateDefinition = new UpdateDefinitionBuilder<EventEntity>().Set(entity => entity.Tournaments, turnaments.ToArray());

            await _entityDataService.Update<EventEntity>(filter => filter.Eq(enity => enity.Id, @event.Id), _ => updateDefinition);
        }
    }
}
