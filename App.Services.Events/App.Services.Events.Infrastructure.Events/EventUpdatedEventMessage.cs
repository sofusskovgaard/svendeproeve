using App.Infrastructure.Events;

namespace App.Services.Events.Infrastructure.Events
{
    public class EventUpdatedEventMessage : IEventMessage
    {
        public string Id { get; set; }
    }
}
