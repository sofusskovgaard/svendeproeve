using App.Infrastructure.Events;

namespace App.Services.Authentication.Infrastructure.Events;

public class UserEmailChangedEventMessage : IEventMessage
{
    public string UserId { get; set; }

    public string Email { get; set; }
}