using App.Infrastructure.Events;

namespace App.Services.Tournaments.Infrastructure.Events;

public class TournamentDeletedEventMessage : IEventMessage
{
    public string Id { get; set; }
}