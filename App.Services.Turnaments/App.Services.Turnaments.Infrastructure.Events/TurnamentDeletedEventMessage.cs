using App.Infrastructure.Events;

namespace App.Services.Turnaments.Infrastructure.Events;

public class TurnamentDeletedEventMessage : IEventMessage
{
    public string Id { get; set; }
}