using App.Infrastructure.Commands;

namespace App.Services.Games.Infrastructure.Commands;

public class DeleteGameCommandMessage : ICommandMessage
{
    public string Id { get; set; }
}