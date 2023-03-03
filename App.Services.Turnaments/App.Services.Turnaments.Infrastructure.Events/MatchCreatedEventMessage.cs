using App.Infrastructure.Events;

namespace App.Services.Turnaments.Infrastructure.Events
{
    public class MatchCreatedEventMessage : IEventMessage
    {
        public string Id { get; set; }
        public string TurnamentId { get; set; }
    }
}
