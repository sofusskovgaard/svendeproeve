using App.Infrastructure.Events;

namespace App.Services.Authentication.Infrastructure.Events;

public class UserUsernameChangedEventMessage : IEventMessage
{
    public string UserId { get; set; }

    public string Username { get; set; }
}