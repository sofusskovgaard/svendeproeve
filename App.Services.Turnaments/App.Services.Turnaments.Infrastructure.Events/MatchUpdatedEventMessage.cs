using App.Infrastructure.Events;

namespace App.Services.Turnaments.Infrastructure.Events;

public class MatchUpdatedEventMessage : IEventMessage
{
    public string Id { get; set; }

    public string Name { get; set; }

    public string[] TeamsId { get; set; }

    public string TurnamentId { get; set; }

    public string WinningTeamId { get; set; }
}