using App.Infrastructure.Commands;

namespace App.Services.Teams.Infrastructure.Commands;

public class DeleteTeamCommandMessage : ICommandMessage
{
    public string Id { get; set; }
}