using App.Infrastructure.Events;

namespace App.Services.Events.Infrastructure.Events;

public class EventCreatedEventMessage : IEventMessage
{
    public string Id { get; set; }
}