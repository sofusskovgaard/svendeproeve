using App.Infrastructure.Events;

namespace App.Services.Users.Infrastructure.Events;

public class UserCreatedEventMessage : IEventMessage
{
    public string Id { get; set; }

    public string? ConnectionId { get; set; }
}