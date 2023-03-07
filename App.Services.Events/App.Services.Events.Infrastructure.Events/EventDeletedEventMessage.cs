using App.Infrastructure.Events;

namespace App.Services.Events.Infrastructure.Events;

public class EventDeletedEventMessage : IEventMessage
{
    public string Id { get; set; }
}