using App.Infrastructure.Events;

namespace App.Services.Turnaments.Infrastructure.Events
{
    public class MatchDeletedEventMessage : IEventMessage
    {
        public string Id { get; set; }
    }
}
