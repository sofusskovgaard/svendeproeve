using App.Infrastructure.Events;

namespace App.Services.Games.Infrastructure.Events;

public class GameDeletedEventMessage : IEventMessage
{
    public string Id { get; set; }
}