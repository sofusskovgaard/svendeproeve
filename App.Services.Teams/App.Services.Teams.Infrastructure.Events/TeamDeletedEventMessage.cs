using App.Infrastructure.Events;

namespace App.Services.Teams.Infrastructure.Events;

public class TeamDeletedEventMessage : IEventMessage
{
    public string Id { get; set; }
}