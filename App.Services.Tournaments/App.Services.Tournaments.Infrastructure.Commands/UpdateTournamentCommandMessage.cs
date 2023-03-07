using App.Infrastructure.Commands;

namespace App.Services.Tournaments.Infrastructure.Commands;

public class UpdateTournamentCommandMessage : ICommandMessage
{
    public string Id { get; set; }

    public string Name { get; set; }

    public string GameId { get; set; }

    public string[] MatchesId { get; set; }
}