using App.Infrastructure.Commands;

namespace App.Services.Tournaments.Infrastructure.Commands;

public class DeleteMatchCommandMessage : ICommandMessage
{
    public string Id { get; set; }
}