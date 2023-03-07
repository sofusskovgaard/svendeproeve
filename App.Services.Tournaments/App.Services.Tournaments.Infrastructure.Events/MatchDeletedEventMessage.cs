using App.Infrastructure.Events;

namespace App.Services.Tournaments.Infrastructure.Events;

public class MatchDeletedEventMessage : IEventMessage
{
    public string Id { get; set; }
}