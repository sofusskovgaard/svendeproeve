using App.Infrastructure.Events;

namespace App.Services.Turnaments.Infrastructure.Events
{
    public class TurnamentCreatedEventMessage : IEventMessage
    {
        public string Id { get; set; }
        public string EventId { get; set; }
    }
}
