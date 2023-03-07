using App.Infrastructure.Events;

namespace App.Services.Tournaments.Infrastructure.Events;

public class TournamentCreatedEventMessage : IEventMessage
{
    public string Id { get; set; }

    public string EventId { get; set; }
}