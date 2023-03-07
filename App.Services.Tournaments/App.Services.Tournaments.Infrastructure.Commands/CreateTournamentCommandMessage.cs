using App.Infrastructure.Commands;

namespace App.Services.Tournaments.Infrastructure.Commands;

public class CreateTournamentCommandMessage : ICommandMessage
{
    public string Name { get; set; }

    public string GameId { get; set; }

    public string EventId { get; set; }
}