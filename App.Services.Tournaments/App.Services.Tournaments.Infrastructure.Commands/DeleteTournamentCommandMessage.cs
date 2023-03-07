using App.Infrastructure.Commands;

namespace App.Services.Tournaments.Infrastructure.Commands;

public class DeleteTournamentCommandMessage : ICommandMessage
{
    public string Id { get; set; }
}