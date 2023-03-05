using App.Infrastructure.Commands;

namespace App.Services.Events.Infrastructure.Commands;

public class DeleteEventCommandMessage : ICommandMessage
{
    public string Id { get; set; }
}